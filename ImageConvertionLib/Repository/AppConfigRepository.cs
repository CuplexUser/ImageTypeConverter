using System;
using System.Threading;
using AutoMapper;
using ImageConverterLib.Models;

namespace ImageConverterLib.Repository
{
    public class AppConfigRepository : RepositoryBase
    {
        private ApplicationSettingsModel _appSettings;
        private ApplicationSettingsModel _cmpSettings;
        private bool _isDirty;
        private IMapper _mapper;

        public AppConfigRepository(IMapper mapper)
        {
            _mapper = mapper;
            _appSettings= new ApplicationSettingsModel();
        }

        public bool LoadSettings()
        {
            return false;
        }

        public bool SaveSettings()
        {
            return false;
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings.
        /// </value>
        public ApplicationSettingsModel AppSettings
        {
            get
            {
                if (_appSettings == null)
                {
                    LoadSettings();
                }
                _isDirty = EvaluateIsDirty();
                return _appSettings;
            }
        }

        private bool EvaluateIsDirty()
        {
            return false;
        }

        /// <summary>
        /// Occurs when [load settings completed].
        /// </summary>
        public event EventHandler LoadSettingsCompleted;
        /// <summary>
        /// Occurs when [save settings completed].
        /// </summary>
        public event EventHandler SaveSettingsCompleted;

        public void NotifySettingsChanged()
        {
        }

        public bool IsDirty { get; set; }

        /// <summary>
        /// Called when [load settings completed].
        /// </summary>
        private void OnLoadSettingsCompleted()
        {
            LoadSettingsCompleted?.Invoke(this, EventArgs.Empty);
            _cmpSettings = _appSettings;
        }

        /// <summary>
        /// Called when [save settings completed].
        /// </summary>
        private void OnSaveSettingsCompleted()
        {
            SaveSettingsCompleted?.Invoke(this, EventArgs.Empty);
            _cmpSettings = _appSettings;
        }
    }
}