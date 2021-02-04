using System;
using System.Collections.Generic;
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
    public class ImageConverterService : ServiceBase
    {
        private readonly ILifetimeScope _scope;
        private readonly IMapper _mapper;
        private List<BatchItemModel> _batchItems;
        private CancellationToken _cancellationToken;
        private BatchWorkflowProgress _progress;
        private bool runWorkerThread = true;
        private Task _mainTask;

        public event BatchCompletedEventHandler OnBatchCompleted;

        public ImageConverterService(ILifetimeScope scope, IMapper mapper)
        {
            _scope = scope;
            _mapper = mapper;
            _batchItems = new List<BatchItemModel>();
        }

        public bool IsRunning { get; set; }

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

                var batchItem = new BatchItemModel(inputFile, outputFile);
                _batchItems.Add(batchItem);

            }
        }

        public void ProcessBatch(BatchWorkflowProgress progress)
        {
            if (IsRunning)
            {
                throw new InvalidOperationException("ProcessBatch was called when already processing a batch");
            }

            _mainTask?.Dispose();
            _mainTask = null;
            _progress = progress;

            try
            {
                IsRunning = true;
                runWorkerThread = true;
                _cancellationToken = new CancellationToken(false);
                _mainTask = new TaskFactory<bool>(_cancellationToken).StartNew(ProcessBatchTaskRunner, _cancellationToken);
            }
            catch (Exception exception)
            {
                Log.Error(exception, "ProcessBatch Exception");
            }
            finally
            {
                IsRunning = false;
            }


          
        }

        private bool ProcessBatchTaskRunner()
        {
            var starTime = DateTime.Now;
            var converter = new ImageConverter();
            int index = 1;
            int filesToConvert = _batchItems.Count;
            string text = "";
            foreach (var batchItem in _batchItems)
            {
                bool outputFileWritten = converter.ConvertImage(batchItem.InputFile,batchItem.OutputFile);
                batchItem.IsCompleted = outputFileWritten;

                if (index == _batchItems.Count)
                {
                    text = "Completed image convertion job.";
                }

                _progress?.Report(new ImageEncodingProgressHandler {FilesCompleted = index ,FileCount = filesToConvert , Text = text});
                index++;

                if (!runWorkerThread)
                {
                    break;
                }
            }

            TimeSpan completionTime = DateTime.Now - starTime;
            OnBatchCompleted?.Invoke(new BatchEventArgs() {SuccessFul = true, TimeSpentProcessing = completionTime });
            return true;
        }
    }
}