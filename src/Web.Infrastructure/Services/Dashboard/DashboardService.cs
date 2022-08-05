using DCMS.Web.Infrastructure.Extensions;
using DCMS.Shared.Wrapper;
using System.Net.Http;
using System.Threading.Tasks;
using DCMS.Application.Features.Dashboards.Queries.GetData;

namespace DCMS.Web.Infrastructure.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<DashboardDataModel>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DashboardEndpoints.GetData);
            var data = await response.ToResult<DashboardDataModel>();
            return data;
        }
    }
}