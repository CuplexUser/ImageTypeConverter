using System;
using System.Windows.Forms;
using Autofac;
using ImageConverterLib.Models;
using ImageConverterLib.Repository;
using JetBrains.Annotations;
using Serilog;

namespace ImageConverterLib.Services
{
    [UsedImplicitly]
    public sealed class ApplicationSettingsService : ServiceBase
    {
        
        private readonly AppConfigRepository _appConfigRepository;

        public string CompanyName { get; } = Application.CompanyName;

        public string ProductName { get; } = Application.ProductName;
        private ApplicationSettingsModel _applicationSettings;
        
        private readonly ILifetimeScope _scope;

        public ApplicationSettingsService(AppConfigRepository appConfigRepository, ILifetimeScope scope)
        {

            _scope = scope;

            _appConfigRepository = appConfigRepository;

            try
            {
                bool result = _appConfigRepository.LoadSettings();
                if (!result)
                {
                    _appConfigRepository.SaveSettings();
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Fatal error encountered when accessing the registry settings");
             

            }

            _appConfigRepository.LoadSettingsCompleted += _appSettingsFileRepository_LoadSettingsCompleted;
        }



        public void SetSettingsStateModified()
        {
            _appConfigRepository.NotifySettingsChanged();
        }

        public ApplicationSettingsModel Settings
        {
            get
            {
                while (_applicationSettings == null)
                {
                    if (!LoadLocalStorageSettings())
                        throw new InvalidOperationException();

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

                LoadLocalStorageSettings();
                OnSettingsLoaded?.Invoke(this, EventArgs.Empty);
                loadedSuccessively = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ErrorLoading AppSettings");
            }

            return loadedSuccessively;
        }



        private bool LoadLocalStorageSettings()
        {
            if (_applicationSettings != null && !_appConfigRepository.IsDirty)
                return true;

            return _appConfigRepository.LoadSettings();
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
                result = _applicationSettings.SaveSettings(_applicationSettings);
                if (!result)
                {
                    return false;
                }
                
                

                OnSettingsSaved?.Invoke(this, new EventArgs());

            }
            catch (Exception ex)
            {
                Log.Error(ex, "SaveSettings threw en exception on");
                return false;
            }

            return result;
        }

    }
}