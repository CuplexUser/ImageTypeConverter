using System.Runtime.Serialization;

namespace ImageConverterLib.DataModels
{
    /// <summary>
    ///   PointDataModel
    /// </summary>
    [DataContract(Name = "PointDataModel")]
    public class PointDataModel
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember(Name = "X", Order = 1)]
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember(Name = "Y", Order = 2)]
        public int Y { get; set; }
    }
}