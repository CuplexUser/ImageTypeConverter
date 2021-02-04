﻿using System;
using System.Drawing;

namespace ImageConverterLib.Models
{
    /// <summary>
    ///   FormStateModel
    /// </summary>
    public class FormStateModel
    {
        /// <summary>
        /// Gets or sets the name of the form.
        /// </summary>
        /// <value>
        /// The name of the form.
        /// </value>
        public string FormName { get; set; }
        /// <summary>
        /// Gets or sets the type of the form.
        /// </summary>
        /// <value>
        /// The type of the form.
        /// </value>
        public Type FormType { get; set; }
        /// <summary>
        /// Gets or sets the size of the form.
        /// </summary>
        /// <value>
        /// The size of the form.
        /// </value>
        public Size FormSize { get; set; }
        /// <summary>
        /// Gets or sets the form position.
        /// </summary>
        /// <value>
        /// The form position.
        /// </value>
        public Point FormPosition { get; set; }
        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        /// <value>
        /// The state of the window.
        /// </value>
        public FormState WindowState { get; set; }
    }

    /// <summary>
    ///   FormState
    /// </summary>
    public enum FormState
    {
        Normal=0,
        /// <summary>A minimized window.</summary>
        Minimized=1,
        /// <summary>A maximized window.</summary>
        Maximized=2,
    }
}