using System.Collections.Generic;

namespace ImageConverterLib.Models
{
    public class UserConfigModel
    {
        public UserConfigModel()
        {

        }

        public UserConfigModel(List<ImageModel> imageModels, string outputDirectory)
        {
            ImageModels = imageModels;
            OutputDirectory = outputDirectory;
        }


        public List<ImageModel> ImageModels { get; private set; }

        public string OutputDirectory { get; set; }

    }
}