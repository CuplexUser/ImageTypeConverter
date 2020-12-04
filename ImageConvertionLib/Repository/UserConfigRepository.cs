using Autofac;
using AutoMapper;

namespace ImageConverterLib.Repository
{
    public class UserConfigRepository: RepositoryBase
    {
        private readonly ILifetimeScope _scope;
        private readonly IMapper _mapper;

        public UserConfigRepository(ILifetimeScope scope, IMapper mapper)
        {
            _scope = scope;
            _mapper = mapper;
        }

        public void SaveConfiguration()
        {

        }
    }
}