using DCMS.Application.Models.Audit;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.Application.Interfaces.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditModel>>> GetCurrentUserTrailsAsync(string userId);

        Task<IResult<string>> ExportToExcelAsync(string userId, string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}