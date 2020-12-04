using System.Collections.Generic;

namespace ImageConverterLib.Models
{
    public class UserConfigModel
    {
        public List<ImageModel> ImageModels { get; set; }

        public string OutputDirectory { get; set; }

    }
}