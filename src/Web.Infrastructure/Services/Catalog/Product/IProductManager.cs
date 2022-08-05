using DCMS.Application.Features.Products.Commands.AddEdit;
using DCMS.Application.Features.Products.Queries.GetAllPaged;
using DCMS.Application.Requests.Catalog;
using DCMS.Shared.Wrapper;
using System.Threading.Tasks;

namespace DCMS.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}