using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using DCMS.Web.Infrastructure.Extensions;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DCMS.Web.Infrastructure.Services.Identity.Roles
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;

        public RoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.RolesEndpoints.Delete}/{id}");
            return await response.ToResult<string>();
        }

        public async Task<IResult<List<RoleModel>>> GetRolesAsync()
        {
            var response = await _httpClient.GetAsync(Routes.RolesEndpoints.GetAll);
            return await response.ToResult<List<RoleModel>>();
        }

        public async Task<IResult<string>> SaveAsync(RoleRequest role)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.RolesEndpoints.Save, role);
            return await response.ToResult<string>();
        }

        public async Task<IResult<PermissionModel>> GetPermissionsAsync(string roleId)
        {
            var response = await _httpClient.GetAsync(Routes.RolesEndpoints.GetPermissions + roleId);
            return await response.ToResult<PermissionModel>();
        }

        public async Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(Routes.RolesEndpoints.UpdatePermissions, request);
            return await response.ToResult<string>();
        }
    }
}