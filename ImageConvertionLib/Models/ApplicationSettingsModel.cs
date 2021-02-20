using System;
using System.Collections.Generic;

namespace ImageConverterLib.Models
{
    public class ApplicationSettingsModel : IEquatable<ApplicationSettingsModel>
    {
        public string OutputDirectory { get; set; }

        public string InputDirectory { get; set; }

        public string ImageFormatExtension { get; set; }

        public DateTime LastAppStartTime { get; set; }

        public IDictionary<string, FormStateModel> FormStateModels { get; set; }

        public bool Equals(ApplicationSettingsModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return OutputDirectory == other.OutputDirectory && InputDirectory == other.InputDirectory && ImageFormatExtension == other.ImageFormatExtension && LastAppStartTime.Equals(other.LastAppStartTime) && Equals(FormStateModels, other.FormStateModels);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ApplicationSettingsModel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (OutputDirectory != null ? OutputDirectory.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InputDirectory != null ? InputDirectory.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ImageFormatExtension != null ? ImageFormatExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ LastAppStartTime.GetHashCode();
                hashCode = (hashCode * 397) ^ (FormStateModels != null ? FormStateModels.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}