using System;
using System.IO;
using Autofac;
using AutoMapper;
using ImageConverterLib.Configuration;
using ImageConverterLib.Repository;
using ImageTypeConverter.UnitTest.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageTypeConverter.UnitTest.Repositories
{

    /// <summary>
    /// AppSettingsRepo Unit tests
    /// </summary>
    [TestClass]
    public class AppSettingsRepoTest
    {
        /// <summary>
        /// The repository
        /// </summary>
        private AppSettingsRepository _repository;
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// The container
        /// </summary>
        private static IContainer _container;

        private ILifetimeScope _scope;

        /// <summary>
        /// Initializes the test class.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void InitTestClass(TestContext context)
        {
            GlobalUnitTestConfig.Initialize(context);
            ApplicationBuildConfig.SetOverrideUserDataPath(GlobalUnitTestConfig.TempDataPath);
            _container = AutofacConfig.CreateContainer();
            context.Properties.Add("Container", _container);

            context.WriteLine("Test Class configuration completed");
        }

        [TestInitialize]
        public void TestInit()
        {
            Assert.IsNull(_scope,"Invalid state reached. _scope not null before init");
            _scope = _container.BeginLifetimeScope();
            _repository = _scope.Resolve<AppSettingsRepository>();
            _mapper = _scope.Resolve<IMapper>();

            var tempDir = new DirectoryInfo(GlobalUnitTestConfig.TempDataPath);

            if (!tempDir.Exists)
            {
                   throw new InvalidProgramException("Local temp data directoty was not found!");
            }
        }

        [TestCleanup]
        public void CleanupTestContext()
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

            model.InputDirectory = new DirectoryInfo("c:\\temp").FullName;
            model.OutputDirectory = GlobalUnitTestConfig.UnitTestOutputPath;

            var result = _repository.SaveSettings(model);

        }


        /// <summary>
        /// Applications the settings disk load test.
        /// </summary>
        [TestMethod]
        public void LoadAppSettingsFromDisk()
        {
            // instantiate default memory model
            var model = AppSettingsRepository.GetDefaultApplicationSettings();

            Assert.IsNotNull(model.FormStateModels, "AppSettingsRepository failed to create valied default config" , nameof(model.FormStateModels) );



        }
    }
}
