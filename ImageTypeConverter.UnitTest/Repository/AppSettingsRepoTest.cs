using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using AutoMapper;
using ImageConverterLib.Models;
using ImageConverterLib.Repository;
using ImageTypeConverter.UnitTest.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;

namespace ImageTypeConverter.UnitTest.Repository
{

    /// <summary>
    /// AppSettingsRepo Unit tests
    /// </summary>
    [TestClass]
    public class AppSettingsRepoTest : RepositoryTestBase
    {
        [ClassInitialize]
        public static void AppRepositoryTestClassInit(TestContext context)
        {
            RepositoryTestClassInit(context);
        }

        [ClassCleanup]
        public static void AppSettingsRepoClassCleanup()
        {
            RepositoryTestClassCleanup();
        }

        [TestInitialize]
        public override void RepositoryTestInit()
        {
            Assert.IsNull(_scope, "Invalid state reached. _scope not null before init");
            _scope = _container.BeginLifetimeScope();
            _repository = _scope.Resolve<AppSettingsRepository>();
            _mapper = _scope.Resolve<IMapper>();
        }

        [TestCleanup]
        public override void RepositoryTestCleanup()
        {
            _scope.Dispose();
            _scope = null;
        }

        /// <summary>
        /// Verifies the default settings model.
        /// </summary>
        [TestMethod]
        public void SaveAppSettingsToDisk()
        {
            var model = AppSettingsRepository.GetDefaultApplicationSettings();

            model.InputDirectory = GlobalUnitTestConfig.TestDataInputPath;
            model.OutputDirectory = GlobalUnitTestConfig.TempDataPath;

            var result = _repository.SaveSettings(model);

            Assert.IsTrue(result, "Save AppSettingsFailed");

            var fi = new FileInfo(Path.Combine(model.OutputDirectory, GlobalUnitTestConfig.AppSettingsFilename));
            Log.Information("Saving App settings in: " + Path.GetFullPath(fi.FullName));
            Assert.IsTrue(fi.Exists, "Could not find ImageConverterSettings.bin at: " + fi.FullName);

        }


        /// <summary>
        /// Applications the settings disk load test.
        /// </summary>
        [TestMethod]
        public void LoadAppSettingsFromDisk()
        {
            // instantiate default memory model
            var model = AppSettingsRepository.GetDefaultApplicationSettings();
            model.ImageFormatExtension = ".webp";


            model.InputDirectory = GlobalUnitTestConfig.TestDataInputPath;
            model.OutputDirectory = GlobalUnitTestConfig.TempDataPath;

            Assert.IsNotNull(model.FormStateModels, "AppSettingsRepository failed to create valied default config", nameof(model.FormStateModels));
            Assert.IsTrue(_repository.SaveSettings(model), "Failed to Save default Settings");

            var loadedSettings = _repository.LoadSettings();

            Assert.IsNotNull(loadedSettings, "Loaded Settings where null.");

            // Do not compare form state dictionaries.
            model.FormStateModels = loadedSettings.FormStateModels;

            // Test each property
            bool modelEquals = model.Equals(loadedSettings);
            Assert.IsTrue(modelEquals, "model.Equals(loadedSettings) The loaded model where not identical to the saved model");
        }

        [TestMethod]
        public void FormStateTest()
        {
            var settingsModel = new ApplicationSettingsModel()
            {
                ImageFormatExtension = ".jpg",
                FormStateModels = new Dictionary<string, FormStateModel>(),
                LastAppStartTime = DateTime.Now,
                InputDirectory = "",
                OutputDirectory = ""
            };

            FormStateModel formStateModel = new FormStateModel()
            {
                FormName = "TestForm",
                FormPosition = new Point(32,128),
                WindowState = FormState.Maximized,
                FormSize = new Size(247,377)
            };

            settingsModel.FormStateModels.Add(formStateModel.FormName, formStateModel);
            Assert.IsTrue(_repository.SaveSettings(settingsModel), "SaveSettings(settingsModel) failed");

            var loadedSettings = _repository.LoadSettings();

            FormStateModel reconstructedFormState = loadedSettings.FormStateModels.Count > 0 ? loadedSettings.FormStateModels.Values.First() : null;
            Assert.IsNotNull(reconstructedFormState,"reconstructedFormState != null");

            // Test each property
            bool objectsAreEqual = reconstructedFormState.Equals(formStateModel);
            Assert.IsTrue(objectsAreEqual, "Reconstructed object not equal to original object.");
        }   
    }
}
