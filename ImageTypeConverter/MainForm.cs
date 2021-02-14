using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using ImageConverterLib.Configuration;
using ImageConverterLib.ImageProcessing.Models;
using ImageConverterLib.Library;
using ImageConverterLib.Library.DataFlow;
using ImageConverterLib.Models;
using ImageConverterLib.Services;
using ImageTypeConverter.Properties;
using Serilog;

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
        ///     The Image converter service
        /// </summary>
        private readonly ImageConverterService _converterService;

        /// <summary>
        ///     The scope
        /// </summary>
        private readonly ILifetimeScope _scope;

        /// <summary>
        ///     The user configuration service
        /// </summary>
        private readonly UserConfigService _userConfigService;

        private bool _ListDropActive;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        /// <param name="applicationSettingsService">The application settings service.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="userConfigService">The user configuration service.</param>
        /// <param name="converterService"></param>
        public MainForm(ApplicationSettingsService applicationSettingsService, ILifetimeScope scope, UserConfigService userConfigService, ImageConverterService converterService)
        {
            _applicationSettingsService = applicationSettingsService;
            _scope = scope;
            _userConfigService = userConfigService;
            _converterService = converterService;
            InitializeComponent();
        }

        private void InitializeForm()
        {
            var imageTypeCollection = SupportedImageTypeOutputFormats.Create();

            imageFormatModelBindingSource.SuspendBinding();
            imageFormatModelBindingSource.DataSource = imageTypeCollection.GetImgFormatModels();
            cboxImageFormat.DataSource = imageFormatModelBindingSource;
            cboxImageFormat.DisplayMember = imageTypeCollection.DisplayMember;
            cboxImageFormat.ValueMember = imageTypeCollection.ValueMember;
            imageFormatModelBindingSource.ResetBindings(true);
            imageFormatModelBindingSource.ResumeBinding();

            if (cboxImageFormat.Items.Count > 0)
                cboxImageFormat.SelectedIndex = 0;

            openFileDialog.Filter = "WEBP Images (*.webp)|*.webp|Tif images (*.tiff;*.tif)|*.tiff;*.tif|Jpeg Images (*.jpg; *.jpeg)|*.jpg;*.jpeg|Png Images (*.png)|*.png|Bitmap Images (*.bmp)|*.bmp|" +
                                    "All files (*.*)|*.*";

            UpdateControlStateFromUserConfig();
        }

        private void UpdateControlStateFromUserConfig()
        {
            if (lnkOutputDirectory.Text != _applicationSettingsService.Settings.OutputDirectory)
            {
                lnkOutputDirectory.Text = _applicationSettingsService.Settings.OutputDirectory;
                lnkOutputDirectory.Links.Clear();
                lnkOutputDirectory.Links.Add(0, lnkOutputDirectory.Text.Length, lnkOutputDirectory.Text);
            }

            if (!string.IsNullOrEmpty(_applicationSettingsService.Settings.InputDirectory)) openFileDialog.InitialDirectory = _applicationSettingsService.Settings.InputDirectory;

            string setExtenstion = _applicationSettingsService.Settings.ImageFormatExtension;
            string selectedText = (cboxImageFormat.SelectedItem as ImageFormatModel).Extension;
            if (!string.IsNullOrEmpty(setExtenstion) && selectedText != setExtenstion)
            {
                foreach (var item in cboxImageFormat.Items)
                {
                    if (item is ImageFormatModel model && model.Extension == setExtenstion)
                    {
                        cboxImageFormat.SelectedItem = model;
                        break;
                    }
                }

            }
        }

        private void UpdateMenuState()
        {
            startBatchConvertionToolStripMenuItem.Enabled = !_converterService.IsRunningBatch;
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

                if (Directory.Exists(targetDir)) Process.Start("explorer", targetDir);
            }
        }

        private void SelectOutputDir()
        {
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                _userConfigService.SetOutputFolder(folderBrowserDialog.SelectedPath);
                _applicationSettingsService.Settings.OutputDirectory = folderBrowserDialog.SelectedPath;
                UpdateControlStateFromUserConfig();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
            _converterService.OnBatchCompleted += converterService_OnBatchCompleted;
            lblStatusLabel.Text = "";
            _applicationSettingsService.LoadSettings();
            UpdateControlStateFromUserConfig();

            Text = Resources.MainTitle + $" - Version {Assembly.GetExecutingAssembly().GetName().Version}";
            RestoreFormState(_applicationSettingsService.Settings);
        }

        private void RestoreFormState(ApplicationSettingsModel settings)
        {
            if (settings.FormStateModels.ContainsKey(Name))
            {
                try
                {
                    var model = settings.FormStateModels[Name];
                    Width = model.FormSize.Width;
                    Height = model.FormSize.Height;

                    Location = new Point(model.FormPosition.X, model.FormPosition.Y);
                    WindowState = (FormWindowState)model.WindowState;
                }
                catch (Exception exception)
                {
                    Log.Error(exception, "RestoreFormState Exception");
                }
            }
        }


        private void converterService_OnBatchCompleted(BatchEventArgs args)
        {
            Invoke(new BatchCompletedEventHandler(BatchComplete), this, new EventArgs());
        }


        private void BatchComplete(object sender, EventArgs e)
        {
            lblStatusLabel.Text = "";
            ConvertProgress.Value = ConvertProgress.Maximum;
            ClearSourceImageList(false);
            UpdateMenuState();
        }

        /// <summary>
        ///     Adds the images using file open dialog.
        /// </summary>
        private void AddImagesUsingFileOpenDialog()
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var selectedFiles = openFileDialog.FileNames.ToList();
                var messageQueue = EventMessageQueue.CreateEventMessageQueue();

                foreach (string filePath in selectedFiles)
                {
                    // validation for filepath being unique.
                    bool isValid = _userConfigService.AddImageToProcessQueue(filePath, ref messageQueue);

                    if (!isValid)
                    {
                        foreach (string message in messageQueue.GetMessageEnumerable())
                            MessageBox.Show(message, "Failed to add image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                if (selectedFiles.Count > 0)
                {
                    _applicationSettingsService.Settings.InputDirectory = Path.GetDirectoryName(selectedFiles[selectedFiles.Count - 1]);
                }

                imageModelBindingSource.SuspendBinding();
                imageModelBindingSource.DataSource = _userConfigService.Config.ImageModels;
                imageModelBindingSource.Sort = "SortOrder";
                imageModelBindingSource.ResumeBinding();
                imageModelBindingSource.ResetBindings(true);
            }
        }

        /// <summary>
        ///     Removes the selected image from queue.
        /// </summary>
        private void RemoveSelectedImageFromQueue()
        {
            if (DataGridImgConvertQueue?.SelectedRows.Count == 0)
            {
                Log.Debug("Remove input queue item failed because no grid view items where selected.");
                return;
            }


            var messageQueue = EventMessageQueue.CreateEventMessageQueue();
            var selectedRows = DataGridImgConvertQueue.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
                if (row.DataBoundItem is ImageModel model)
                {
                    if (!_userConfigService.RemoveImageFromProcessQueue(model.UniqueId, ref messageQueue))
                    {
                        foreach (string message in messageQueue.GetMessageEnumerable()) MessageBox.Show(message, "Failed to remove enqueued item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            imageModelBindingSource.SuspendBinding();
            imageModelBindingSource.DataSource = _userConfigService.Config.ImageModels;
            imageModelBindingSource.Sort = "SortOrder";
            imageModelBindingSource.ResetBindings(true);
            imageModelBindingSource.ResumeBinding();
        }

        private void MoveSelectedRows(bool moveUp)
        {
            var selectedRows = DataGridImgConvertQueue.SelectedRows;

            if (selectedRows.Count == 0 || selectedRows.Count == imageModelBindingSource.Count) return;


            List<Guid> rowSelection = new List<Guid>();
            foreach (DataGridViewRow row in selectedRows)
                if (row.DataBoundItem is ImageModel model)
                    rowSelection.Add(model.UniqueId);
                else
                    return;

            if (!_userConfigService.ChangeListPosition(rowSelection, moveUp)) return;

            imageModelBindingSource.SuspendBinding();
            imageModelBindingSource.DataSource = _userConfigService.Config.ImageModels;
            imageModelBindingSource.ResumeBinding();
            imageModelBindingSource.ResetBindings(true);

            // Reselect items
            for (int i = 0; i < DataGridImgConvertQueue.Rows.Count; i++) DataGridImgConvertQueue.Rows[i].Selected = false;

            foreach (var model in rowSelection.Select(guid => _userConfigService.GetImageModelById(guid))) DataGridImgConvertQueue.Rows[model.SortOrder].Selected = true;
        }

        private void ClearSourceImageList(bool prompt)
        {
            if (_userConfigService.Config.ImageModels.Count > 0)
            {
                if (prompt)
                {
                    bool result = MessageBox.Show($"Are you sure you want to clear the list with {_userConfigService.Config.ImageModels.Count} items", "Clear List?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
                    if (!result)
                        return;
                }

                imageModelBindingSource.SuspendBinding();
                _userConfigService.ClearProcessQueue();
                imageModelBindingSource.DataSource = _userConfigService.Config.ImageModels;
                imageModelBindingSource.ResumeBinding();
                imageModelBindingSource.ResetBindings(true);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormStateModel model;
            if (_applicationSettingsService.Settings.FormStateModels == null) _applicationSettingsService.Settings.FormStateModels = new ConcurrentDictionary<string, FormStateModel>();

            if (_applicationSettingsService.Settings.FormStateModels.ContainsKey(Name))
                model = _applicationSettingsService.Settings.FormStateModels[Name];
            else
            {
                model = new FormStateModel();
                _applicationSettingsService.Settings.FormStateModels.Add(Name, model);
            }

            model.FormPosition = Location;
            model.FormSize = new Size(Width, Height);
            model.FormName = Name;
            model.FormType = typeof(MainForm);

            switch (WindowState)
            {
                case FormWindowState.Normal:
                    model.WindowState = FormState.Normal;
                    break;
                case FormWindowState.Minimized:
                    model.WindowState = FormState.Minimized;
                    break;
                case FormWindowState.Maximized:
                    model.WindowState = FormState.Maximized;
                    model.FormSize = MinimumSize;
                    break;
                default:
                    model.WindowState = FormState.Normal;
                    break;
            }

            if (cboxImageFormat.SelectedItem is ImageFormatModel formatModel)
                _applicationSettingsService.Settings.ImageFormatExtension = formatModel.Extension;

            _applicationSettingsService.Settings.OutputDirectory = lnkOutputDirectory.Text;
            _applicationSettingsService.SaveSettings();
            e.Cancel = false;
        }

        private delegate void UpdateProgressBar(object sender, ImageEncodingProgress e);

        private delegate void BatchCompletedEventHandler(object sender, EventArgs eventArgs);


        #region FormCallbackFunctions

        /// <summary>
        ///     Handles the Click event of the btnUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveSelectedRows(true);
        }

        /// <summary>
        ///     Handles the Click event of the btnDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveSelectedRows(false);
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
            RemoveSelectedImageFromQueue();
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
            if (MessageBox.Show("Are you sure you want to close the application?", "Exit?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) Application.Exit();
        }

        /// <summary>
        ///     Handles the Click event of the clearListToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSourceImageList(true);
        }

        /// <summary>
        ///     Handles the Click event of the clearResultOutputToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void clearResultOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtConversionResults.Clear();
        }

        /// <summary>
        ///     Handles the Click event of the startBatchConversionToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void startBatchConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_userConfigService.ProcessQueueLength == 0)
            {
                MessageBox.Show("Convert image list is empty!", "Nothing to do.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ConvertProgress.Minimum = 0;
            ConvertProgress.Maximum = 100;
            lblStatusLabel.Text = "";
            _userConfigService.Config.OutputFileExtension = (cboxImageFormat.SelectedItem as ImageFormatModel).Extension;
            _userConfigService.Config.OutputDirectory = lnkOutputDirectory.Text;
            _converterService.InitBatch(_userConfigService.Config, _userConfigService.Config.OutputDirectory);
            var progress = new BatchWorkflowProgress(new Progress<ImageEncodingProgress>(Handler));
            //ProgressBar.
            _converterService.ProcessBatch(progress);
            //UpdateMenuState();
        }

        private void Handler(ImageEncodingProgress obj)
        {
            Invoke(new UpdateProgressBar(LocalThreadUpdateProgressBar), this, obj);
        }

        private void LocalThreadUpdateProgressBar(object sender, ImageEncodingProgress e)
        {
            ConvertProgress.Value = e.ProgressPercentage;

            lblStatusLabel.Text = e.Text.Length >= 64 ? e.Text.Substring(0, 64) + "..." : e.Text;

            txtConversionResults.AppendText(e.Text + "\n");
        }

        /// <summary>
        ///     Handles the Click event of the aboutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutApplicationBox().ShowDialog(this);
        }

        /// <summary>
        ///     Handles the Click event of the settingsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog(this);
            settingsForm.Dispose();
        }

        #endregion

        #region Drag & Drop Event Handlers

        private void lstImageConvertQueue_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void lstImageConvertQueue_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                _ListDropActive = true;
            else
                _ListDropActive = false;
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
                    e.Effect = DragDropEffects.Link;
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