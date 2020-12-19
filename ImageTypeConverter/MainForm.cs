using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using ImageConverterLib.Configuration;
using ImageConverterLib.Library.DataFlow;
using ImageConverterLib.Models;
using ImageConverterLib.Services;

namespace ImageTypeConverter
{
    /// <summary>
    ///     MainForm
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainForm : Form
    {
        /// <summary>
        ///     The application settings service
        /// </summary>
        private readonly ApplicationSettingsService _applicationSettingsService;

        /// <summary>
        ///     The scope
        /// </summary>
        private readonly ILifetimeScope _scope;

        /// <summary>
        ///     The user configuration service
        /// </summary>
        private readonly UserConfigService _userConfigService;

        private bool _ListDropActive = false;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        /// <param name="applicationSettingsService">The application settings service.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="userConfigService">The user configuration service.</param>
        public MainForm(ApplicationSettingsService applicationSettingsService, ILifetimeScope scope, UserConfigService userConfigService)
        {
            _applicationSettingsService = applicationSettingsService;
            _scope = scope;
            _userConfigService = userConfigService;
            InitializeComponent();
        }

        private void InitializeForm()
        {
            var imageTypeCollection = ImageFormatCollection.Create();

            imageFormatModelBindingSource.DataSource = imageTypeCollection.ImageTypes;

            if (cboxImageFormat.Items.Count > 0)
            {
                cboxImageFormat.SelectedIndex = 0;
            }
        }

        private void UpdateControlStateFromUserConfig()
        {
            if (lnkOutputDirectory.Text != _userConfigService.Config.OutputDirectory)
            {
                lnkOutputDirectory.Text = _userConfigService.Config.OutputDirectory;
                lnkOutputDirectory.Links.Clear();
                lnkOutputDirectory.Links.Add(0, lnkOutputDirectory.Text.Length, lnkOutputDirectory.Text);
            }
        }


        /// <summary>
        ///     Handles the LinkClicked event of the lnkOutputDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs" /> instance containing the event data.</param>
        private void lnkOutputDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string targetDir = e.Link.LinkData as string;

                if (Directory.Exists(targetDir))
                {
                    Process.Start("explorer", targetDir);
                }
            }
        }

        private void SelectOutputDir()
        {
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                _userConfigService.SetOutputFolder(folderBrowserDialog.SelectedPath);
                UpdateControlStateFromUserConfig();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
            UpdateControlStateFromUserConfig();
        }

        private void AddImagesUsingFileOpenDialog()
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var selectedFiles = openFileDialog.FileNames.ToList();
                var messageQueue = EventMessageQueue.CreateEventMessageQueue();
                
                foreach (string filePath in selectedFiles)
                {
                    // validation for filepath being unique.
                    bool isValid=_userConfigService.AddImageToProcessQueue(filePath, ref messageQueue);

                    if (!isValid)
                    {
                        foreach (string message in messageQueue.GetMessageEnumerable())
                        {
                            MessageBox.Show(message, "Failed to add image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                imageModelBindingSource.DataSource = _userConfigService.Config;
                imageFormatModelBindingSource.ResetBindings(false);
            }
        }


        #region FormCallbackFunctions

        /// <summary>
        ///     Handles the Click event of the btnUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnUp_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Handles the Click event of the btnDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnDown_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddImagesUsingFileOpenDialog();
        }

        /// <summary>
        ///     Handles the Click event of the btnRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the btnBrowseFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            SelectOutputDir();

        }

        #endregion

        #region MainMenuFunctions

        /// <summary>
        ///     Handles the Click event of the openToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the saveToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the saveAsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the toolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddImagesUsingFileOpenDialog();

        }

        /// <summary>
        ///     Handles the Click event of the setOutputFolderToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void setOutputFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectOutputDir();
        }

        /// <summary>
        ///     Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the application?", "Exit?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        /// <summary>
        ///     Handles the Click event of the clearListToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the clearResultOutputToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void clearResultOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the startBatchConversionToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void startBatchConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the aboutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles the Click event of the settingsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Drag & Drop Event Handlers



        private void lstImageConvertQueue_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void lstImageConvertQueue_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                _ListDropActive = true;
            }
            else
            {
                _ListDropActive = false;
            }


        }

        private void lstImageConvertQueue_DragLeave(object sender, EventArgs e)
        {
            _ListDropActive = false;
        }

        private void lstImageConvertQueue_DragOver(object sender, DragEventArgs e)
        {
            if (_ListDropActive)
            {
                if ((e.AllowedEffect & DragDropEffects.Link) > 0)
                {
                    e.Effect = DragDropEffects.Link;
                }


            }
        }

        private void lstImageConvertQueue_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void lstImageConvertQueue_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        #endregion
    }
}