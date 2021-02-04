using System;
using System.Collections.Generic;

namespace ImageConverterLib.Models
{
    public class ApplicationSettingsModel
    {
        public string OutputDirectory { get; set; }

        public string  InputDirectory { get; set; }

        public string  ImageFormatExtension { get; set; }

        public DateTime LastAppStartTime{ get; set; }

        public IDictionary<string, FormStateModel> FormStateModels { get; set; }
    }
}