using System;
using System.IO;
using ImageConverterLib.ImageProcessing.Models;
using ImageConverterLib.Models;
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

        private readonly ApplicationSettingsModel applicationSettings;

        public ImageConverter(ApplicationSettingsModel applicationSettings)
        {
            this.applicationSettings = applicationSettings;
            ImageProcessorBootstrapper.Instance.AddImageFormats(new WebPFormat());
            factory = new ImageFactory();
        }

        public bool ConvertImage(ImageProcessModel sourceModel, ImageProcessModel destinationModel)
        {
            try
            {
                var imgData = factory.Load(sourceModel.FilePath);

                factory.Quality(applicationSettings.JpegImageQuality);
                imgData.Format(GetOutputFormat(destinationModel));
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

        private ISupportedImageFormat GetOutputFormat(ImageProcessModel image)
        {
            //ImageProcessor.Imaging.Formats
            switch (image.Extension)
            {
                case ".jpg":
                case ".jpeg":
                    return new JpegFormat();
                case ".png":
                    return new PngFormat();
                case ".tiff":
                    return new TiffFormat();
                case ".bmp":
                    return new BitmapFormat();
                case ".gif":
                    return new GifFormat();
                case ".webp":
                    return new WebPFormat();

                default:
                    break;
            }


            return null;
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