using AutoMapper;
using DCMS.Application.Features.ExtendedAttributes.Commands.AddEdit;
using DCMS.Application.Features.ExtendedAttributes.Queries.GetAll;
using DCMS.Application.Features.ExtendedAttributes.Queries.GetAllByEntityId;
using DCMS.Application.Features.ExtendedAttributes.Queries.GetById;
using DCMS.Domain.ExtendedAttributes;

namespace DCMS.Application.Mappings
{
    public class ExtendedAttributeProfile : Profile
    {
        public ExtendedAttributeProfile()
        {
            CreateMap(typeof(AddEditExtendedAttributeCommand<,,,>), typeof(DocumentExtendedAttribute))
                .ForMember(nameof(DocumentExtendedAttribute.Entity), opt => opt.Ignore())
                .ForMember(nameof(DocumentExtendedAttribute.CreatedBy), opt => opt.Ignore())
                .ForMember(nameof(DocumentExtendedAttribute.CreatedOn), opt => opt.Ignore())
                .ForMember(nameof(DocumentExtendedAttribute.LastModifiedBy), opt => opt.Ignore())
                .ForMember(nameof(DocumentExtendedAttribute.LastModifiedOn), opt => opt.Ignore());

            CreateMap(typeof(GetExtendedAttributeByIdModel<,>), typeof(DocumentExtendedAttribute)).ReverseMap();
            CreateMap(typeof(GetAllExtendedAttributesModel<,>), typeof(DocumentExtendedAttribute)).ReverseMap();
            CreateMap(typeof(GetAllExtendedAttributesByEntityIdModel<,>), typeof(DocumentExtendedAttribute)).ReverseMap();
        }
    }
}