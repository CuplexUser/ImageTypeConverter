using System;
using System.IO;
using Autofac;
using AutoMapper;
using ImageConverterLib.Configuration;
using ImageConverterLib.Repository;
using ImageTypeConverter.UnitTest.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageTypeConverter.UnitTest.Service
{
    public abstract class ServiceTestBase
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

        protected static void ServiceTestClassInit(TestContext context)
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

            context.WriteLine("Test Class configuration completed");
        }


        protected static void ServiceTestClassCleanup()
        {
            _container?.Dispose();
            _container = null;
        }


        public abstract void ServiceTestInit();


        public abstract void ServiceTestCleanup();
    }
}