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

        public bool Equals(FormStateModel other)
        {
            return FormName == other.FormName && 
                   FormSize.Height == other.FormSize.Height &&
                   FormSize.Width == other.FormSize.Width &&
                   FormPosition.Y == other.FormPosition.Y &&
                   FormPosition.X == other.FormPosition.X &&
                   WindowState == other.WindowState;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (FormName != null ? FormName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ FormSize.GetHashCode();
                hashCode = (hashCode * 397) ^ FormPosition.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)WindowState;
                return hashCode;
            }
        }
    }

    /// <summary>
    ///   FormState
    /// </summary>
    public enum FormState
    {
        Normal = 0,
        /// <summary>A minimized window.</summary>
        Minimized = 1,
        /// <summary>A maximized window.</summary>
        Maximized = 2,
    }
}