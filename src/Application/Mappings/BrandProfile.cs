using AutoMapper;
using DCMS.Application.Features.Brands.Commands.AddEdit;
using DCMS.Application.Features.Brands.Queries.GetAll;
using DCMS.Application.Features.Brands.Queries.GetById;
using DCMS.Domain.Catalog;

namespace DCMS.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}