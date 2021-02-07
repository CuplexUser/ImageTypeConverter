using System.Collections.Generic;
using ImageConverterLib.Models;

namespace ImageConverterLib.Configuration
{
    /// <summary>
    /// SupportedImageTypeOutputFormats for the combobox selection
    /// </summary>
    public class SupportedImageTypeOutputFormats
    {
        private readonly SupportedImageFormatFactory _factory;

        private SupportedImageTypeOutputFormats()
        {
            _factory = SupportedImageFormatFactory.CreateInstance();
            
        }

        public string DisplayMember => nameof(ImageFormatModel.PublicName);

        public string ValueMember => nameof(ImageFormatModel.Guid);

        public IEnumerable<ImageFormatModel> GetImgFormatModels()
        {
            return _factory.GetFormatModels();
        }

        public static SupportedImageTypeOutputFormats Create()
        {
            return new SupportedImageTypeOutputFormats();
        }
    }
}
