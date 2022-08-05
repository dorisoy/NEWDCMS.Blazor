using AutoMapper;
using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;

namespace DCMS.Web.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionModel, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimModel, RoleClaimRequest>().ReverseMap();
        }
    }
}