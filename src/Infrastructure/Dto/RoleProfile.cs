using AutoMapper;
using DCMS.Infrastructure.Models.Identity;
using DCMS.Application.Models.Identity;

namespace DCMS.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleModel, AppRole>().ReverseMap();
        }
    }
}