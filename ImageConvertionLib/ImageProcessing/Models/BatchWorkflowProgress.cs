using System;

namespace ImageConverterLib.ImageProcessing.Models
{
    public sealed class BatchWorkflowProgress  : Progress<ImageEncodingProgressHandler>
    {
        private readonly IProgress<ImageEncodingProgress> _batchProcessingProgress;

        public BatchWorkflowProgress()
        {

        }

        public BatchWorkflowProgress(IProgress<ImageEncodingProgress> progress)
        {
            _batchProcessingProgress = progress;
        }

        
        public void Report(ImageEncodingProgressHandler imageEncodingProgress)
        {
            if (_batchProcessingProgress != null)
            {
                var progress = new ImageEncodingProgress();

                if (imageEncodingProgress.FilesCompleted > 0)
                    progress.ProgressPercentage = (int)(imageEncodingProgress.FilesCompleted * 100 / imageEncodingProgress.FileCount);

                progress.Text = imageEncodingProgress.Text;
                _batchProcessingProgress.Report(progress);
            }
            else
            {
                OnReport(imageEncodingProgress);
            }
        }
    }
}