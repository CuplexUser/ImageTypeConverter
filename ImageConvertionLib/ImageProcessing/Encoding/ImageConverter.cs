using System;
using System.IO;
using ImageConverterLib.ImageProcessing.Models;
using ImageProcessor;
using ImageProcessor.Configuration;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Serilog;

namespace ImageConverterLib.ImageProcessing.Encoding
{
    public class ImageConverter : ImageCodecBase, IDisposable
    {
        private readonly ImageFactory factory;

        public ImageConverter()
        {

            ImageProcessorBootstrapper.Instance.AddImageFormats(new WebPFormat());
            factory = new ImageFactory();
        }

        public bool ConvertImage(ImageProcessModel sourceModel, ImageProcessModel destinationModel)
        {
            try
            {
                var imgData = factory.Load(sourceModel.FilePath);
                imgData.Format(new JpegFormat());
                imgData.Save(destinationModel.FilePath);

                if (!File.Exists(destinationModel.FilePath))
                    return false;

                var fi = new FileInfo(destinationModel.FilePath);
                destinationModel.FileSize = fi.Length;
                destinationModel.Extension = fi.Extension;
                destinationModel.FileName = fi.Name;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ConvertImage Exception");
                return false;
            }
            

            return true;
        }

        //public enum ImageFormat
        //{
        //    WebP,
        //    jpeg,
        //    png,
        //    tiff
        //}
        public void Dispose()
        {
            factory?.Dispose();
        }
    }
}