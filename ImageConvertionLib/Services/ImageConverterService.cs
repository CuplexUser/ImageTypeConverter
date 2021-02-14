using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using ImageConverterLib.ImageProcessing.Encoding;
using ImageConverterLib.ImageProcessing.Models;
using ImageConverterLib.Library;
using ImageConverterLib.Models;
using Serilog;

namespace ImageConverterLib.Services
{
    public class ImageConverterService : ServiceBase, IDisposable
    {
        private readonly ILifetimeScope _scope;
        private readonly IMapper _mapper;
        private List<BatchItemModel> _batchItems;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        private BatchWorkflowProgress _progress;
        private bool runWorkerThread = true;
        private Task _mainTask;
        private object lockObject = new object();

        public event BatchCompletedEventHandler OnBatchCompleted;

        public ImageConverterService(ILifetimeScope scope, IMapper mapper)
        {
            _scope = scope;
            _mapper = mapper;
            _batchItems = new List<BatchItemModel>();
            _cancellationTokenSource = new CancellationTokenSource();
            IsRunningBatch = false;
        }

        public bool IsRunningBatch
        {
            get;
            private set;
        }

        public void InitBatch(UserConfigModel config, string outputPath)
        {
            _batchItems = new List<BatchItemModel>();

            config.ImageModels.Sort(ImageComparison.ImageModelComparison);

            for (int i = 0; i < config.ImageModels.Count; i++)
            {
                var imageModel = config.ImageModels[i];
                imageModel.SortOrder = i;

                var inputFile = _mapper.Map<ImageProcessModel>(imageModel);
                var outputFile = _mapper.Map<ImageProcessModel>(imageModel);
                outputFile.DirectoryPath = outputPath;
                outputFile.FileSize = 0;
                outputFile.SortOrder = i;
                outputFile.Extension = config.OutputFileExtension;
                outputFile.FileName = inputFile.FileName;
                
                outputFile.FileName = outputFile.FileName.Replace(inputFile.Extension, outputFile.Extension);
                outputFile.FilePath = Path.Combine(outputFile.DirectoryPath, outputFile.FileName);

                var batchItem = new BatchItemModel(inputFile, outputFile);
                _batchItems.Add(batchItem);

            }
        }

        public bool ProcessBatch(BatchWorkflowProgress progress)
        {
            if (IsRunningBatch)
            {
                return false;
                //throw new InvalidOperationException("ProcessBatch was called when already processing a batch");
            }

            _progress = progress;

            try
            {
                IsRunningBatch = true;
                runWorkerThread = true;
                var token = _cancellationTokenSource.Token;
                _cancellationToken = new CancellationToken(false);
                _mainTask = new TaskFactory<bool>(token).StartNew(ProcessBatchTaskRunner, _cancellationToken);
                _mainTask.GetAwaiter().OnCompleted(DisposeTask);
                return true;
            }
            catch (Exception exception)
            {
                Log.Error(exception, "ProcessBatch Exception");
            }

            return false;
        }

        private void DisposeTask()
        {
            if (_mainTask != null)
            {
                if (_mainTask.IsCompleted)
                {
                    _mainTask.Dispose();
                    _mainTask = null;
                }
                else
                {
                    Log.Warning("DisposeTask: MainTask was not completed");
                }
            }
        }

        public bool AbortBatchRun()
        {
            if (IsRunningBatch)
            {
                runWorkerThread = false;
                _mainTask.Wait(2500, _cancellationTokenSource.Token);
                if (_mainTask.Status == TaskStatus.Running)
                {
                    _cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(250));
                }
            }

            return false;
        }

        private bool ProcessBatchTaskRunner()
        {
            var starTime = DateTime.Now;
            var converter = new ImageConverter();
            int index = 1;
            int filesToConvert = _batchItems.Count;
            foreach (var batchItem in _batchItems)
            {
                bool outputFileWritten = converter.ConvertImage(batchItem.InputFile, batchItem.OutputFile);
                if (!outputFileWritten)
                    Log.Debug($"Failed to convert batchItem: {batchItem.InputFile.FileName} to {batchItem.OutputFile.FileName}");

                batchItem.IsCompleted = outputFileWritten;

                // Update progress
                string text = "";

                if (outputFileWritten)
                {
                    text = $"Converted image source: {batchItem.InputFile.FileName} to destination: {batchItem.OutputFile.FilePath}";
                }
                else
                {
                    text = $"Failed to convert image source: {batchItem.InputFile.FileName} to destination: {batchItem.OutputFile.FilePath}";
                }

                if (index == _batchItems.Count)
                {
                    text += "\nCompleted image batch job.";
                }

                _progress?.Report(new ImageEncodingProgressHandler { FilesCompleted = index, FileCount = filesToConvert, Text = text });
                index++;

                if (!runWorkerThread)
                {
                    break;
                }
            }

            TimeSpan completionTime = DateTime.Now - starTime;
            IsRunningBatch = false;
            OnBatchCompleted?.Invoke(new BatchEventArgs() { SuccessFul = true, TimeSpentProcessing = completionTime });
            return true;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            _mainTask?.Dispose();
        }
    }
}