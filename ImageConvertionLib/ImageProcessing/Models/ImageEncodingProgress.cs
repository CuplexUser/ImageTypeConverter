namespace ImageConverterLib.ImageProcessing.Models
{
    public class ImageEncodingProgress
    {
        private int _progressPercentage;

        public int ProgressPercentage
        {
            get => _progressPercentage;
            set
            {
                if (value >= 0 && value <= 100)
                    _progressPercentage = value;
            }
        }

        public string Text { get; set; }
    }

    public class ImageEncodingProgressHandler
    {
        public long FilesCompleted { get; set; }
        public long FileCount { get; set; }
        public string Text { get; set; }
    }
}