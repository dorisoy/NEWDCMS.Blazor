using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services.Identity.Roles
{
    public interface IRoleService : IService
    {
        Task<IResult<List<RoleModel>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(RoleRequest role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionModel>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}