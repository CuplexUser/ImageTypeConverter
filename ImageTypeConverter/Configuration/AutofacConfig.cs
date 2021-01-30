using System.Reflection;
using Autofac;
using ImageConverterLib.ConfigHelper;


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
            var thisAssembly = Assembly.GetExecutingAssembly();


            Assembly[] coreAssemblies = new Assembly[2];
            var ImageConverterLibAssembly = AssemblyHelper.GetAssembly();

            coreAssemblies[0] = thisAssembly;
            coreAssemblies[1] = ImageConverterLibAssembly;

            for (int i = 0; i < coreAssemblies.Length; i++)
            {
                builder.RegisterAssemblyModules(coreAssemblies[i]);
            }

            var container = builder.Build();


            return container;
        }
    }
}