using System;
using System.IO;
using Autofac;
using AutoMapper;
using ImageConverterLib.Configuration;
using ImageConverterLib.Repository;
using ImageTypeConverter.UnitTest.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageTypeConverter.UnitTest.Repository
{
    /// <summary>
    /// RepositoryTestBase
    /// </summary>
    public abstract class RepositoryTestBase
    {
        /// <summary>
        /// The repository
        /// </summary>
        protected AppSettingsRepository _repository;
        /// <summary>
        /// The mapper
        /// </summary>
        protected IMapper _mapper;

        /// <summary>
        /// The container
        /// </summary>
        protected static IContainer _container;

        /// <summary>
        /// The scope
        /// </summary>
        protected ILifetimeScope _scope;

        /// <summary>
        /// Tests the class initialize.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="InvalidProgramException">Local temp data folder was not found!</exception>
        protected static void RepositoryTestClassInit(TestContext context)
        {
            GlobalUnitTestConfig.Initialize(context);
            ApplicationBuildConfig.SetOverrideUserDataPath(GlobalUnitTestConfig.TempDataPath);
            _container = AutofacConfig.CreateContainer();
            context.Properties.Add("Container", _container);

            var tempDir = new DirectoryInfo(GlobalUnitTestConfig.TempDataPath);

            if (!tempDir.Exists)
            {
                throw new InvalidProgramException("Local temp data folder was not found!");
            }

            context.WriteLine("RepositoryTestClassInit configuration completed");
        }

        /// <summary>
        /// Repositories the test class cleanup.
        /// </summary>
        protected static void RepositoryTestClassCleanup()
        {
            _container?.Dispose();
            _container = null;

            var tempDir = new DirectoryInfo(GlobalUnitTestConfig.TempDataPath);
            var files = tempDir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!file.Extension.Equals(".log",StringComparison.CurrentCultureIgnoreCase))
                    file.Delete();
            }

        }

        /// <summary>
        /// Repositories the test initialize.
        /// </summary>
        public abstract void RepositoryTestInit();

        /// <summary>
        /// Repositories the test cleanup.
        /// </summary>
        public abstract void RepositoryTestCleanup();
    }
}