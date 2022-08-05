using DCMS.Shared.Wrapper;
using System.Threading.Tasks;
using DCMS.Application.Features.Dashboards.Queries.GetData;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services.Dashboard
{
    public interface IDashboardService : IService
    {
        Task<IResult<DashboardDataModel>> GetDataAsync();
    }
}