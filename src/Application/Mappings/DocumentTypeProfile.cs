using AutoMapper;
using DCMS.Application.Features.DocumentTypes.Commands.AddEdit;
using DCMS.Application.Features.DocumentTypes.Queries.GetAll;
using DCMS.Application.Features.DocumentTypes.Queries.GetById;
using DCMS.Domain.Misc;

namespace DCMS.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}