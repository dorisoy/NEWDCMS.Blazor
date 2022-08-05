using DCMS.Application.Features.Documents.Commands.AddEdit;
using DCMS.Application.Features.Documents.Queries.GetAll;
using DCMS.Application.Requests.Documents;
using DCMS.Shared.Wrapper;
using System.Threading.Tasks;
using DCMS.Application.Features.Documents.Queries.GetById;

namespace DCMS.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}