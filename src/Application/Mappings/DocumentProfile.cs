using AutoMapper;
using DCMS.Application.Features.Documents.Commands.AddEdit;
using DCMS.Application.Features.Documents.Queries.GetById;
using DCMS.Domain.Misc;

namespace DCMS.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}