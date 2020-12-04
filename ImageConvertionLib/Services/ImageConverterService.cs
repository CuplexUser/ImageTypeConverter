using Autofac;

namespace ImageConverterLib.Services
{
    public class ImageConverterService : ServiceBase
    {
        private readonly ILifetimeScope _scope;

        public ImageConverterService(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }
}