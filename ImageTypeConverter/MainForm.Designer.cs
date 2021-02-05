
namespace ImageTypeConverter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.setOutputFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearResultOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startBatchConvertionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConvertProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpBoxImgInputSelect = new System.Windows.Forms.GroupBox();
            this.DataGridImgConvertQueue = new System.Windows.Forms.DataGridView();
            this.Extension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirectoryPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.lblDestinationDir = new System.Windows.Forms.Label();
            this.lblOutputFormat = new System.Windows.Forms.Label();
            this.cboxImageFormat = new System.Windows.Forms.ComboBox();
            this.lnkOutputDirectory = new System.Windows.Forms.LinkLabel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.grpBoxResult = new System.Windows.Forms.GroupBox();
            this.txtConversionResults = new System.Windows.Forms.RichTextBox();
            this.lblStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imageFormatModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpBoxImgInputSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridImgConvertQueue)).BeginInit();
            this.panel1.SuspendLayout();
            this.grpBoxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFormatModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.setOutputFolderToolStripMenuItem,
            this.toolStripMenuItem5,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "&Open List";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(183, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.toolStripMenuItem1.Text = "&Add Source Files";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(183, 6);
            // 
            // setOutputFolderToolStripMenuItem
            // 
            this.setOutputFolderToolStripMenuItem.Name = "setOutputFolderToolStripMenuItem";
            this.setOutputFolderToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.setOutputFolderToolStripMenuItem.Text = "Set Output Folder";
            this.setOutputFolderToolStripMenuItem.Click += new System.EventHandler(this.setOutputFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.clearListToolStripMenuItem,
            this.clearResultOutputToolStripMenuItem,
            this.toolStripMenuItem7,
            this.deleteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(247, 6);
            // 
            // clearListToolStripMenuItem
            // 
            this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
            this.clearListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.clearListToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.clearListToolStripMenuItem.Text = "Clear Image Queue";
            this.clearListToolStripMenuItem.Click += new System.EventHandler(this.clearListToolStripMenuItem_Click);
            // 
            // clearResultOutputToolStripMenuItem
            // 
            this.clearResultOutputToolStripMenuItem.Name = "clearResultOutputToolStripMenuItem";
            this.clearResultOutputToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.clearResultOutputToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.clearResultOutputToolStripMenuItem.Text = "Clear Result Output";
            this.clearResultOutputToolStripMenuItem.Click += new System.EventHandler(this.clearResultOutputToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(247, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startBatchConvertionToolStripMenuItem,
            this.toolStripMenuItem6,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // startBatchConvertionToolStripMenuItem
            // 
            this.startBatchConvertionToolStripMenuItem.Name = "startBatchConvertionToolStripMenuItem";
            this.startBatchConvertionToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.startBatchConvertionToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.startBatchConvertionToolStripMenuItem.Text = "Start Batch Convertion";
            this.startBatchConvertionToolStripMenuItem.Click += new System.EventHandler(this.startBatchConversionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(209, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(819, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "MainMenu";
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(3);
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.ConvertProgress,
            this.lblStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(819, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "Conversion Status";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(122, 21);
            this.toolStripStatusLabel1.Text = "Conversion Progress:";
            // 
            // ConvertProgress
            // 
            this.ConvertProgress.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ConvertProgress.Name = "ConvertProgress";
            this.ConvertProgress.Size = new System.Drawing.Size(300, 20);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "WEBP Images|*.webp|Tif images|*.tif";
            this.openFileDialog.InitialDirectory = "D:\\Blandat";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.ShowReadOnly = true;
            // 
            // grpBoxImgInputSelect
            // 
            this.grpBoxImgInputSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpBoxImgInputSelect.Controls.Add(this.DataGridImgConvertQueue);
            this.grpBoxImgInputSelect.Controls.Add(this.panel1);
            this.grpBoxImgInputSelect.Controls.Add(this.btnRemove);
            this.grpBoxImgInputSelect.Controls.Add(this.btnAdd);
            this.grpBoxImgInputSelect.Controls.Add(this.btnDown);
            this.grpBoxImgInputSelect.Controls.Add(this.btnUp);
            this.grpBoxImgInputSelect.Location = new System.Drawing.Point(12, 27);
            this.grpBoxImgInputSelect.Name = "grpBoxImgInputSelect";
            this.grpBoxImgInputSelect.Size = new System.Drawing.Size(465, 339);
            this.grpBoxImgInputSelect.TabIndex = 2;
            this.grpBoxImgInputSelect.TabStop = false;
            this.grpBoxImgInputSelect.Text = "Images To Convert";
            // 
            // DataGridImgConvertQueue
            // 
            this.DataGridImgConvertQueue.AllowDrop = true;
            this.DataGridImgConvertQueue.AllowUserToAddRows = false;
            this.DataGridImgConvertQueue.AllowUserToDeleteRows = false;
            this.DataGridImgConvertQueue.AllowUserToResizeRows = false;
            this.DataGridImgConvertQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridImgConvertQueue.AutoGenerateColumns = false;
            this.DataGridImgConvertQueue.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridImgConvertQueue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridImgConvertQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridImgConvertQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileNameDataGridViewTextBoxColumn,
            this.Extension,
            this.Size,
            this.DirectoryPath});
            this.DataGridImgConvertQueue.DataSource = this.imageModelBindingSource;
            this.DataGridImgConvertQueue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGridImgConvertQueue.Location = new System.Drawing.Point(9, 19);
            this.DataGridImgConvertQueue.Name = "DataGridImgConvertQueue";
            this.DataGridImgConvertQueue.ReadOnly = true;
            this.DataGridImgConvertQueue.RowHeadersVisible = false;
            this.DataGridImgConvertQueue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataGridImgConvertQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridImgConvertQueue.Size = new System.Drawing.Size(397, 250);
            this.DataGridImgConvertQueue.TabIndex = 16;
            // 
            // Extension
            // 
            this.Extension.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Extension.DataPropertyName = "Extension";
            this.Extension.FillWeight = 50F;
            this.Extension.HeaderText = "Extension";
            this.Extension.MinimumWidth = 15;
            this.Extension.Name = "Extension";
            this.Extension.ReadOnly = true;
            // 
            // Size
            // 
            this.Size.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Size.DataPropertyName = "Size";
            this.Size.FillWeight = 50F;
            this.Size.HeaderText = "Size";
            this.Size.MinimumWidth = 15;
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            // 
            // DirectoryName
            // 
            this.DirectoryPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DirectoryPath.DataPropertyName = "DirectoryName";
            this.DirectoryPath.HeaderText = "Directory";
            this.DirectoryPath.MinimumWidth = 20;
            this.DirectoryPath.Name = "DirectoryPath";
            this.DirectoryPath.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.btnBrowseFolder);
            this.panel1.Controls.Add(this.lblDestinationDir);
            this.panel1.Controls.Add(this.lblOutputFormat);
            this.panel1.Controls.Add(this.cboxImageFormat);
            this.panel1.Controls.Add(this.lnkOutputDirectory);
            this.panel1.Location = new System.Drawing.Point(6, 275);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 58);
            this.panel1.TabIndex = 15;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseFolder.Location = new System.Drawing.Point(274, 25);
            this.btnBrowseFolder.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(126, 25);
            this.btnBrowseFolder.TabIndex = 3;
            this.btnBrowseFolder.Text = "Set Output Directory";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // lblDestinationDir
            // 
            this.lblDestinationDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDestinationDir.AutoSize = true;
            this.lblDestinationDir.Location = new System.Drawing.Point(3, 7);
            this.lblDestinationDir.Name = "lblDestinationDir";
            this.lblDestinationDir.Size = new System.Drawing.Size(58, 13);
            this.lblDestinationDir.TabIndex = 14;
            this.lblDestinationDir.Text = "Output Dir:";
            // 
            // lblOutputFormat
            // 
            this.lblOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOutputFormat.AutoSize = true;
            this.lblOutputFormat.Location = new System.Drawing.Point(3, 31);
            this.lblOutputFormat.Name = "lblOutputFormat";
            this.lblOutputFormat.Size = new System.Drawing.Size(62, 13);
            this.lblOutputFormat.TabIndex = 8;
            this.lblOutputFormat.Text = "Img Format:";
            // 
            // cboxImageFormat
            // 
            this.cboxImageFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxImageFormat.CausesValidation = false;
            this.cboxImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxImageFormat.FormattingEnabled = true;
            this.cboxImageFormat.Location = new System.Drawing.Point(72, 28);
            this.cboxImageFormat.Margin = new System.Windows.Forms.Padding(4, 3, 3, 3);
            this.cboxImageFormat.Name = "cboxImageFormat";
            this.cboxImageFormat.Size = new System.Drawing.Size(160, 21);
            this.cboxImageFormat.TabIndex = 6;
            // 
            // lnkOutputDirectory
            // 
            this.lnkOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkOutputDirectory.AutoSize = true;
            this.lnkOutputDirectory.Location = new System.Drawing.Point(69, 7);
            this.lnkOutputDirectory.Margin = new System.Windows.Forms.Padding(3, 6, 3, 5);
            this.lnkOutputDirectory.MaximumSize = new System.Drawing.Size(375, 30);
            this.lnkOutputDirectory.Name = "lnkOutputDirectory";
            this.lnkOutputDirectory.Size = new System.Drawing.Size(81, 13);
            this.lnkOutputDirectory.TabIndex = 9;
            this.lnkOutputDirectory.TabStop = true;
            this.lnkOutputDirectory.Text = "OutputDirectory";
            this.lnkOutputDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOutputDirectory_LinkClicked);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(412, 112);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(45, 25);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(412, 81);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 25);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Location = new System.Drawing.Point(412, 50);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(45, 25);
            this.btnDown.TabIndex = 11;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Location = new System.Drawing.Point(412, 19);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(45, 25);
            this.btnUp.TabIndex = 10;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // grpBoxResult
            // 
            this.grpBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxResult.Controls.Add(this.txtConversionResults);
            this.grpBoxResult.Location = new System.Drawing.Point(481, 27);
            this.grpBoxResult.Name = "grpBoxResult";
            this.grpBoxResult.Padding = new System.Windows.Forms.Padding(5);
            this.grpBoxResult.Size = new System.Drawing.Size(326, 339);
            this.grpBoxResult.TabIndex = 3;
            this.grpBoxResult.TabStop = false;
            this.grpBoxResult.Text = "Conversion Results";
            // 
            // txtConversionResults
            // 
            this.txtConversionResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConversionResults.Location = new System.Drawing.Point(5, 18);
            this.txtConversionResults.Margin = new System.Windows.Forms.Padding(5);
            this.txtConversionResults.Name = "txtConversionResults";
            this.txtConversionResults.ReadOnly = true;
            this.txtConversionResults.Size = new System.Drawing.Size(316, 316);
            this.txtConversionResults.TabIndex = 0;
            this.txtConversionResults.Text = "";
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(88, 21);
            this.lblStatusLabel.Text = "[lblStatusLabel]";
            // 
            // fileNameDataGridViewTextBoxColumn
            // 
            this.fileNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileNameDataGridViewTextBoxColumn.DataPropertyName = "FileName";
            this.fileNameDataGridViewTextBoxColumn.FillWeight = 130F;
            this.fileNameDataGridViewTextBoxColumn.HeaderText = "File Name";
            this.fileNameDataGridViewTextBoxColumn.MinimumWidth = 20;
            this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
            this.fileNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // imageModelBindingSource
            // 
            this.imageModelBindingSource.DataSource = typeof(ImageConverterLib.Models.ImageModel);
            // 
            // imageFormatModelBindingSource
            // 
            this.imageFormatModelBindingSource.DataSource = typeof(ImageConverterLib.Models.ImageFormatModel);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 391);
            this.Controls.Add(this.grpBoxResult);
            this.Controls.Add(this.grpBoxImgInputSelect);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(835, 430);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Type Batch Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpBoxImgInputSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridImgConvertQueue)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpBoxResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFormatModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar ConvertProgress;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox grpBoxImgInputSelect;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.ComboBox cboxImageFormat;
        private System.Windows.Forms.Label lblOutputFormat;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.LinkLabel lnkOutputDirectory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem setOutputFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblDestinationDir;
        private System.Windows.Forms.ToolStripMenuItem startBatchConvertionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearResultOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpBoxResult;
        private System.Windows.Forms.RichTextBox txtConversionResults;
        private System.Windows.Forms.BindingSource imageFormatModelBindingSource;
        private System.Windows.Forms.DataGridView DataGridImgConvertQueue;
        private System.Windows.Forms.BindingSource imageModelBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Extension;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirectoryPath;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusLabel;
    }
}

