using AutoMapper;
using ImageConverterLib.Repository;

namespace ImageConverterLib.Services
{
    public class UserConfigService : ServiceBase
    {
        private readonly UserConfigRepository _userConfigRepository;
        private readonly IMapper _mapper;

        public UserConfigService(IMapper mapper, UserConfigRepository userConfigRepository)
        {
            _mapper = mapper;
            _userConfigRepository = userConfigRepository;
        }
    }
}