using System.Reflection;
using Autofac;
using ImageConverterLib.Configuration;
using ImageConverterLib.Library.AutofacModules;
using ImageTypeConverter.Library.AutofacModules;


namespace ImageTypeConverter.Configuration
{
    public static class AutofacConfig
    {
        /// <summary>
        /// Creates the container.
        /// </summary>
        /// <returns></returns>
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            var thisAssembly = GetMainAssembly();


            Assembly[] coreAssemblies = new Assembly[2];
            var ImageConverterLibAssembly = ImageConverterLib.Configuration.AssemblyHelper.GetAssembly();

            coreAssemblies[0] = thisAssembly;
            coreAssemblies[1] = ImageConverterLibAssembly;

            if (ImageConverterLibAssembly != null)
            {
                builder.RegisterAssemblyModules(ImageConverterLibAssembly);
            }

            builder.RegisterAssemblyModules(thisAssembly);
            var container = builder.Build();


            return container;
        }

        /// <summary>
        /// Gets the main assembly.
        /// </summary>
        /// <returns></returns>
        public static Assembly GetMainAssembly()
        {
            return typeof(ImageConverterGUIModule).Assembly;
        }
    }
}