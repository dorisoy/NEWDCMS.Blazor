using AutoMapper;
using DCMS.Infrastructure.Models.Audit;
using DCMS.Application.Models.Audit;

namespace DCMS.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditModel, Audit>().ReverseMap();
        }
    }
}