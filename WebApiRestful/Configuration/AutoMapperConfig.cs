using AutoMapper;
using WebApiRestful.Domain.Entities;
using WebApiRestful.ViewModel;

namespace WebApiRestful.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserModel, ApplicationUser>()
                      .ForMember(dest => dest.PasswordHash, y => y.MapFrom(src => src.Password))
                      .ReverseMap();
        }
    }
}
