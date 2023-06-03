using AutoMapper;
using WebApiRestful.Domain.Entities;
using WebApiRestful.ViewModel;

namespace WebApiRestful.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserModel, User>()
                      .ForMember(dest => dest.DisplayName, y => y.MapFrom(src => src.Fullname))
                      .ReverseMap();
        }
    }
}
