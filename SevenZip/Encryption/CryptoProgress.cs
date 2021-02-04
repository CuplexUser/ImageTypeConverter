using System;
using SevenZip.Storage.Models;

namespace SevenZip.Encryption
{
    public sealed class CryptoProgress : Progress<CryptoProgressHandler>
    {
        private readonly IProgress<StorageManagerProgress> _storageManagerAsyncExProgress;

        public CryptoProgress()
        {
        }

        public CryptoProgress(IProgress<StorageManagerProgress> storageManagerAsyncExProgress)
        {
            _storageManagerAsyncExProgress = storageManagerAsyncExProgress;
        }

        public void Report(CryptoProgressHandler cryptoProgressHandler)
        {
            if (_storageManagerAsyncExProgress != null)
            {
                StorageManagerProgress progress = new StorageManagerProgress();

                if (cryptoProgressHandler.TotalBytes > 0)
                    progress.ProgressPercentage = (int)((cryptoProgressHandler.EncodedBytes * 100) / cryptoProgressHandler.TotalBytes);

                progress.Text = cryptoProgressHandler.Text;
                _storageManagerAsyncExProgress.Report(progress);
            }
            else
                OnReport(cryptoProgressHandler);
        }
    }

    public class CryptoProgressHandler
    {
        public long EncodedBytes { get; set; }
        public long TotalBytes { get; set; }
        public string Text { get; set; }
    }
}