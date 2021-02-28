using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageConverterLib.Models
{
    /// <summary>
    ///    ApplicationSettingsModel
    /// </summary>
    /// <seealso cref="IEqualityComparer{ApplicationSettingsModel}" />
    public class ApplicationSettingsModel : IEquatable<ApplicationSettingsModel>
    {
        /// <summary>
        /// Gets or sets the output directory.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
        public string OutputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the input directory.
        /// </summary>
        /// <value>
        /// The input directory.
        /// </value>
        public string InputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the image format extension.
        /// </summary>
        /// <value>
        /// The image format extension.
        /// </value>
        public string ImageFormatExtension { get; set; }

        /// <summary>
        /// Gets or sets the JPEG image quality.
        /// </summary>
        /// <value>
        /// The JPEG image quality.
        /// </value>
        [Range(typeof(Int32), "50", "100", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int JpegImageQuality { get; set; }

        /// <summary>
        /// Gets or sets the last application start time.
        /// </summary>
        /// <value>
        /// The last application start time.
        /// </value>
        public DateTime LastAppStartTime { get; set; }

        /// <summary>
        /// Gets or sets the form state models.
        /// </summary>
        /// <value>
        /// The form state models.
        /// </value>
        public IDictionary<string, FormStateModel> FormStateModels { get; set; }

        /// <summary>
        /// Equals the specified model.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool Equals(ApplicationSettingsModel x, ApplicationSettingsModel y)
        {
            if (x.GetType() != y.GetType()) return false;
            return x.OutputDirectory == y.OutputDirectory &&
                   x.InputDirectory == y.InputDirectory &&
                   x.ImageFormatExtension == y.ImageFormatExtension &&
                   x.JpegImageQuality == y.JpegImageQuality &&
                   x.LastAppStartTime.Equals(y.LastAppStartTime) &&
                   Equals(x.FormStateModels,
                       y.FormStateModels);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(ApplicationSettingsModel other)
        {
            return Equals(this,other);
        }
    }
}