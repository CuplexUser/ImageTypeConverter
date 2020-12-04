using System.Reflection;
using System.Windows.Forms;
using Autofac;
using Module = Autofac.Module;

namespace ImageTypeConverter.Library.AutofacModules
{
    /// <summary>
    /// Autofac Module: ImageConverterModule
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class ImageConverterGUIModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                            .AssignableTo<Form>()
                            .AsSelf()
                            .InstancePerDependency();
        }
    }
}