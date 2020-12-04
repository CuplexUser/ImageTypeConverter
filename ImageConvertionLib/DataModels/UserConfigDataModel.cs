using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ImageConverterLib.DataModels
{
    [DataContract(Name = "UserConfigDataModel")]
    public class UserConfigDataModel
    {
        [DataMember(Name = "ImageDataModels", Order = 1)]
        public List<ImageDataModel> ImageDataModels { get; set; }
    }
}