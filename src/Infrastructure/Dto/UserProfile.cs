using AutoMapper;
using DCMS.Infrastructure.Models.Identity;
using DCMS.Application.Models.Identity;

namespace DCMS.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, AppUser>().ReverseMap();
            CreateMap<ChatUserModel, AppUser>().ReverseMap()
                .ForMember(dest => dest.EmailAddress, source => source.MapFrom(source => source.Email)); //Specific Mapping
        }
    }
}