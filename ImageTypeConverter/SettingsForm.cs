using System;
using System.Text;
using System.Windows.Forms;
using ImageConverterLib.Helpers;
using ImageConverterLib.Models;
using ImageConverterLib.Services;

namespace ImageTypeConverter
{
    public partial class SettingsForm : Form
    {
        private ApplicationSettingsService _settingsService;
        private ApplicationSettingsModel model;
        private ModelValidator _validator;

        public void Init(ApplicationSettingsService settingsService)
        {
            _settingsService = settingsService;
            _settingsService.LoadSettings();
            model = settingsService.Settings;
            _validator = ApplicationSettingsService.CreateModelValidator(model);
            BindControlValues();
        }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void BindControlValues()
        {
            JpegImgQualityUpDown.Value = Math.Max(JpegImgQualityUpDown.Minimum, Math.Min(JpegImgQualityUpDown.Maximum, model.JpegImageQuality));
        }

        private void ReadControlValues()
        {
            model.JpegImageQuality = Convert.ToInt32(JpegImgQualityUpDown.Value);
        }

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            FocusControlToolTip.SetToolTip(JpegImgQualityUpDown, "Jpeg ImageQuality: Min 50, Max 100");
            FocusControlToolTip.Active = false;
        }

        private void JpegImgQualityUpDown_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            model.JpegImageQuality = Convert.ToInt32(JpegImgQualityUpDown.Value);
            bool valid = _validator.ValidateModel();

            FocusControlToolTip.Active = !valid;
            e.Cancel = !valid;
        }


        private void btnOk_Click(object sender, System.EventArgs e)
        {
            ReadControlValues();

            if (_validator.ValidateModel())
            {
                DialogResult = DialogResult.OK;
                _settingsService.Settings.JpegImageQuality = model.JpegImageQuality;

            }
            else
            {
                var sb = new StringBuilder();
                foreach (var result in _validator.ValidationResults)
                {
                    sb.AppendLine(result.ErrorMessage);
                }

                MessageBox.Show("Could not save settings\n" + sb, "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}