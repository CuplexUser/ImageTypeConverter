using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Autofac;
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


        #region FormCallbackFunctions


        /// <summary>
        ///     Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateControlStateFromUserConfig();
        }




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
    }
}