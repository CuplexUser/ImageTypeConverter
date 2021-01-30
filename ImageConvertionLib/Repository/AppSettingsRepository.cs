using System;
using System.IO;
using AutoMapper;
using ImageConverterLib.ConfigHelper;
using ImageConverterLib.DataModels;
using ImageConverterLib.Models;
using ImageConverterLib.Providers;

namespace ImageConverterLib.Repository
{
    public class AppSettingsRepository : RepositoryBase
    {
        private readonly FileSystemIOProvider _ioProvider;
        private string appConfigSettingsFilePath;
        private const string settingsFilename = " ImageConverterSettings.bin";
        private IMapper _mapper;

        public AppSettingsRepository(IMapper mapper)
        {
            _mapper = mapper;
            _ioProvider = new FileSystemIOProvider();
            appConfigSettingsFilePath = Path.Combine(GlobalSettings.GetUserDataDirectoryPath(), settingsFilename);
        }

        public ApplicationSettingsModel LoadSettings()
        {
            var userConfig = _ioProvider.LoadApplicationSettings(appConfigSettingsFilePath);

            var settings=  _mapper.Map<ApplicationSettingsModel>(userConfig);

            OnLoadSettingsCompleted();
            return settings;
        }

        public bool SaveSettings(ApplicationSettingsModel settings)
        {
            ApplicationSettingsDataModel settingsDataModel = _mapper.Map<ApplicationSettingsModel, ApplicationSettingsDataModel>(settings);
            bool result= _ioProvider.SaveApplicationSettings(appConfigSettingsFilePath, settingsDataModel);
            OnSaveSettingsCompleted();
            return result;
        }
          


        /// <summary>
        /// Occurs when [load settings completed].
        /// </summary>
        public event EventHandler LoadSettingsCompleted;
        /// <summary>
        /// Occurs when [save settings completed].
        /// </summary>
        public event EventHandler SaveSettingsCompleted;

        public bool IsDirty { get; set; }

        /// <summary>
        /// Called when [load settings completed].
        /// </summary>
        protected void OnLoadSettingsCompleted()
        {
            LoadSettingsCompleted?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when [save settings completed].
        /// </summary>
        protected void OnSaveSettingsCompleted()
        {
            SaveSettingsCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}