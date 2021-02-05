using System;
using System.IO;
using System.Windows.Forms;
using ImageConverterLib.Models;
using ImageConverterLib.Repository;
using JetBrains.Annotations;
using Serilog;

namespace ImageConverterLib.Services
{
    [UsedImplicitly]
    public sealed class ApplicationSettingsService : ServiceBase
    {

        private readonly AppSettingsRepository _appSettingsRepository;

        public string CompanyName { get; } = Application.CompanyName;

        public string ProductName { get; } = Application.ProductName;
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
                    LoadLocalStorageSettings();
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
            _appSettingsRepository.LoadSettings();

            return _applicationSettings;
        }


        private void _appSettingsFileRepository_LoadSettingsCompleted(object sender, EventArgs e)
        {
            if (_applicationSettings != null)
            {
                _applicationSettings = new ApplicationSettingsModel();
            }

        }
        public bool SaveSettings()
        {
            bool result = true;

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