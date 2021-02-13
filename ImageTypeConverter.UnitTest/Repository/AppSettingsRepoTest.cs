using System;
using System.IO;
using Autofac;
using AutoMapper;
using ImageConverterLib.Configuration;
using ImageConverterLib.Repository;
using ImageTypeConverter.UnitTest.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using Serilog.Core;

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
        public void SaveAppSettingsOnDisk()
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

            Assert.IsNull(model.FormStateModels, "AppSettingsRepository failed to create valied default config", nameof(model.FormStateModels));



        }
    }
}
