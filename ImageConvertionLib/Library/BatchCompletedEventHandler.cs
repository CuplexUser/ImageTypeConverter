using System;

namespace ImageConverterLib.Library
{
    public delegate void BatchCompletedEventHandler(BatchEventArgs args);


    public class BatchEventArgs : EventArgs
    {
        public TimeSpan TimeSpentProcessing { get; set; }

        public bool SuccessFul { get; set; }

    }
}