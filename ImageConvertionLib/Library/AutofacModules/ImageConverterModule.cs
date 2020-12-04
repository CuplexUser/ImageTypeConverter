using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using AutoMapper;
using ImageConverterLib.Repository;
using ImageConverterLib.Services;
using Module = Autofac.Module;

namespace ImageConverterLib.Library.AutofacModules
{
    /// <summary>
    /// Autofac Module: ImageConverterModule
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class ImageConverterModule : Module
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
            builder.RegisterAssemblyTypes(typeof(ServiceBase).Assembly)
                   .AssignableTo<ServiceBase>()
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(RepositoryBase).Assembly)
                   .AssignableTo<RepositoryBase>()
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .SingleInstance();



            builder.Register(context => context.Resolve<MapperConfiguration>()
                    .CreateMapper())
                .As<IMapper>()
                .AutoActivate()
                .SingleInstance();

            builder.Register(Configure)
                            .AutoActivate()
                            .AsSelf()
                            .AsImplementedInterfaces()
                            .SingleInstance();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                            .AssignableTo<Form>()
                            .AsSelf()
                            .InstancePerDependency();
        }

        /// <summary>
        /// Configures the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private static MapperConfiguration Configure(IComponentContext context)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                var innerContext = context.Resolve<IComponentContext>();
                cfg.ConstructServicesUsing(innerContext.Resolve);

                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            });

            return configuration;
        }
    }
}