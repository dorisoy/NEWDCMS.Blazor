using DCMS.Application.Models.Audit;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCMS.Shared.Services;


namespace DCMS.Web.Infrastructure.Services.Audit
{
    public interface IAuditService : IService
    {
        Task<IResult<IEnumerable<AuditModel>>> GetCurrentUserTrailsAsync();

        Task<IResult<string>> DownloadFileAsync(string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}