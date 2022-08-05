using AutoMapper;
using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using DCMS.Infrastructure.Models.Identity;

namespace DCMS.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimModel, AppRoleClaim>()
                .ForMember(nameof(AppRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(AppRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, AppRoleClaim>()
                .ForMember(nameof(AppRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(AppRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}