using System.Collections.Generic;
using System.Threading.Tasks;
using DCMS.Application.Features.DocumentTypes.Commands.AddEdit;
using DCMS.Application.Features.DocumentTypes.Queries.GetAll;
using DCMS.Shared.Wrapper;

namespace DCMS.Client.Infrastructure.Managers.Misc.DocumentType
{
    public interface IDocumentTypeManager : IManager
    {
        Task<IResult<List<GetAllDocumentTypesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDocumentTypeCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}