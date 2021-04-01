using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ImageConverterLib.Helpers;
using ImageConverterLib.Models;
using ImageConverterLib.Repository;
using JetBrains.Annotations;
using Serilog;

namespace ImageConverterLib.Services
{
    public class ApplicationSettingsService : ServiceBase
    {
        private readonly AppSettingsRepository _appSettingsRepository;
        private ApplicationSettingsModel _applicationSettings;

        public ApplicationSettingsService(AppSettingsRepository appSettingsRepository)
        {
            _appSettingsRepository = appSettingsRepository;

            try
            {
                _applicationSettings = _appSettingsRepository.LoadSettings();
                if (_applicationSettings == null)
                {
                    _applicationSettings = AppSettingsRepository.GetDefaultApplicationSettings();
                    _appSettingsRepository.SaveSettings(_applicationSettings);
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Fatal error encountered when accessing the registry settings");
                throw new IOException("Application Settings could not be loaded and could not be set to default and saved");
            }

            _appSettingsRepository.LoadSettingsCompleted += _appSettingsFileRepository_LoadSettingsCompleted;
        }

        public void SetSettingsStateModified()
        {

        }

        public ApplicationSettingsModel Settings
        {
            get
            {
                if (_applicationSettings == null)
                {
                    _applicationSettings = LoadLocalStorageSettings();
                }

                return _applicationSettings;
            }
            private set => _applicationSettings = value;
        }


        public event EventHandler OnSettingsLoaded;
        public event EventHandler OnSettingsSaved;


        public bool LoadSettings()
        {
            bool loadedSuccessively = false;

            try
            {
                _applicationSettings = _appSettingsRepository.LoadSettings();
                ValidateSettings();
                OnSettingsLoaded?.Invoke(this, EventArgs.Empty);
                loadedSuccessively = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ErrorLoading AppSettings");
            }

            return loadedSuccessively;
        }

        private ApplicationSettingsModel LoadLocalStorageSettings()
        {
            LoadSettings();
            return _applicationSettings;
        }

        private void ValidateSettings()
        {
            var defSettings = AppSettingsRepository.GetDefaultApplicationSettings();
            ModelValidator validator = new ModelValidator(_applicationSettings);
            if (!validator.ValidateModel())
            {
                Log.Warning("Loaded application settings are invalid. {ErrorMessage}", validator.ValidationResults.First().ErrorMessage);
                if (_applicationSettings.JpegImageQuality < 50 || _applicationSettings.JpegImageQuality > 100)
                {
                    _applicationSettings.JpegImageQuality = defSettings.JpegImageQuality;
                    Log.Debug("JpegImageQuality was invalid. Value changed to: {JpegImageQuality}", _applicationSettings.JpegImageQuality);
                }

                if (string.IsNullOrEmpty(_applicationSettings.InputDirectory))
                {
                    _applicationSettings.InputDirectory = defSettings.InputDirectory;
                    Log.Debug("InputDirectory was invalid. Value changed to: {InputDirectory}", _applicationSettings.InputDirectory);
                }

                SaveSettings();
            }
        }

        private void _appSettingsFileRepository_LoadSettingsCompleted(object sender, EventArgs e)
        {
            if (_applicationSettings != null)
            {
                _applicationSettings = new ApplicationSettingsModel();
            }
        }

        public static ModelValidator CreateModelValidator(ApplicationSettingsModel model)
        {
            var modelValidator = new ModelValidator(model);
            return modelValidator;
        }

        public bool SaveSettings()
        {
            bool result;

            if (_applicationSettings == null)
            {
                throw new InvalidOperationException("Cant save uninitialized Null settings");
            }

            try
            {
                result = _appSettingsRepository.SaveSettings(_applicationSettings);
                if (result)
                    OnSettingsSaved?.Invoke(this, new EventArgs());

            }
            catch (Exception ex)
            {
                Log.Error(ex, "SaveSettings threw en exception on");
                result = false;
            }

            return result;
        }
    }
}