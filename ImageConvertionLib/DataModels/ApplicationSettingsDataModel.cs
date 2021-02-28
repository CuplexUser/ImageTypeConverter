using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ImageConverterLib.DataModels
{
    /// <summary>
    ///    ApplicationSettingsDataModel
    /// </summary>
    [DataContract(Name = "ApplicationSettingsDataModel")]
    public class ApplicationSettingsDataModel
    {
        /// <summary>
        /// Gets or sets the output directory.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
        [DataMember(Name = "OutputDirectory", Order = 1)]
        public string OutputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the input directory.
        /// </summary>
        /// <value>
        /// The input directory.
        /// </value>
        [DataMember(Name = "InputDirectory", Order = 2)]
        public string InputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the image format extension.
        /// </summary>
        /// <value>
        /// The image format extension.
        /// </value>
        [DataMember(Name = "ImageFormatExtension", Order = 3)]
        public string ImageFormatExtension { get; set; }

        /// <summary>
        /// Gets or sets the last application start time.
        /// </summary>
        /// <value>
        /// The last application start time.
        /// </value>
        [DataMember(Name = "LastAppStartTime", Order = 4)]
        public DateTime LastAppStartTime { get; set; }

        /// <summary>
        /// Gets or sets the JPEG image quality.
        /// </summary>
        /// <value>
        /// The JPEG image quality.
        /// </value>
        [DataMember(Name = "JpegImageQuality", Order = 5)]
        public int JpegImageQuality { get; set; }

        /// <summary>
        /// Gets or sets the form state data models.
        /// </summary>
        /// <value>
        /// The form state data models.
        /// </value>
        [DataMember(Name = "FormStateDataModels", Order = 6)]
        public IList<FormStateDataModel> FormStateDataModels { get; set; }  
    }
}