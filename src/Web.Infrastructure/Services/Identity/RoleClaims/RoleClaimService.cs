using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using DCMS.Web.Infrastructure.Extensions;
using DCMS.Shared.Wrapper;

namespace DCMS.Web.Infrastructure.Services.Identity.RoleClaims
{
    public class RoleClaimService : IRoleClaimService
    {
        private readonly HttpClient _httpClient;

        public RoleClaimService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.RoleClaimsEndpoints.Delete}/{id}");
            return await response.ToResult<string>();
        }

        public async Task<IResult<List<RoleClaimModel>>> GetRoleClaimsAsync()
        {
            var response = await _httpClient.GetAsync(Routes.RoleClaimsEndpoints.GetAll);
            return await response.ToResult<List<RoleClaimModel>>();
        }

        public async Task<IResult<List<RoleClaimModel>>> GetRoleClaimsByRoleIdAsync(string roleId)
        {
            var response = await _httpClient.GetAsync($"{Routes.RoleClaimsEndpoints.GetAll}/{roleId}");
            return await response.ToResult<List<RoleClaimModel>>();
        }

        public async Task<IResult<string>> SaveAsync(RoleClaimRequest role)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.RoleClaimsEndpoints.Save, role);
            return await response.ToResult<string>();
        }
    }
}