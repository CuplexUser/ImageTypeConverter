﻿using System.Runtime.Serialization;

namespace ImageConverterLib.DataModels
{
    /// <summary>
    ///      FormStateDataModel
    /// </summary>
    [DataContract(Name = "FormStateDataModel")]
    public class FormStateDataModel
    {
        /// <summary>
        /// Gets or sets the name of the form.
        /// </summary>
        /// <value>
        /// The name of the form.
        /// </value>
        [DataMember(Name = "FormName", Order = 1)]
        public string FormName { get; set; }

        /// <summary>
        /// Gets or sets the size of the form.
        /// </summary>
        /// <value>
        /// The size of the form.
        /// </value>
        [DataMember(Name = "FormSize", Order = 2)]
        public VectorDataModel FormSize { get; set; }

        /// <summary>
        /// Gets or sets the form position.
        /// </summary>
        /// <value>
        /// The form position.
        /// </value>
        [DataMember(Name = "FormPosition", Order = 3)]
        public PointDataModel FormPosition { get; set; }

        /// <summary>
        /// Gets or sets the state of the windows.
        /// </summary>
        /// <value>
        /// The state of the windows.
        /// </value>
        [DataMember(Name = "WindowsState", Order = 4)]
        public int WindowsState { get; set; }
    }
}