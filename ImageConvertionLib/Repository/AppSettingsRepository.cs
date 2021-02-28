using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using ImageConverterLib.ConfigHelper;
using ImageConverterLib.DataModels;
using ImageConverterLib.Models;
using ImageConverterLib.Providers;
using Serilog;

namespace ImageConverterLib.Repository
{
    public class AppSettingsRepository : RepositoryBase
    {
        private readonly FileSystemIOProvider _ioProvider;
        private string appConfigSettingsFilePath;
        private const string settingsFilename = "ImageConverterSettings.bin";
        private IMapper _mapper;

        public AppSettingsRepository(IMapper mapper)
        {
            _mapper = mapper;
            _ioProvider = new FileSystemIOProvider();

            appConfigSettingsFilePath = Path.Combine(GlobalSettings.Settings.GetUserDataDirectoryPath(), settingsFilename);
        }

        public static ApplicationSettingsModel GetDefaultApplicationSettings()
        {
            var settings = new ApplicationSettingsModel
            {
                ImageFormatExtension = "",
                InputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                OutputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                LastAppStartTime = DateTime.Now,
                JpegImageQuality = 85,
                FormStateModels = new Dictionary<string, FormStateModel>()
            };

            return settings;
        }

        public ApplicationSettingsModel LoadSettings()
        {
            var applicationConfig = _ioProvider.LoadApplicationSettings(appConfigSettingsFilePath);
            var settings =  _mapper.Map<ApplicationSettingsModel>(applicationConfig);

            if (settings == null) 
            {
                settings = GetDefaultApplicationSettings();
                Log.Information("Creating default Application Settings");
            }

            if (settings.FormStateModels == null)
            {
                settings.FormStateModels = new ConcurrentDictionary<string, FormStateModel>();
            }

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