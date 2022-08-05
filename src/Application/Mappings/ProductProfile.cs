using AutoMapper;
using DCMS.Application.Features.Products.Commands.AddEdit;
using DCMS.Domain.Catalog;

namespace DCMS.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}