using DCMS.Application.Interfaces.Common;
using DCMS.Application.Models.Identity;
using DCMS.Application.Requests.Identity;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.Application.Interfaces.Services.Identity
{
    public interface IRoleService : IService
    {
        Task<Result<List<RoleModel>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleModel>> GetByIdAsync(string id);

        Task<Result<string>> SaveAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(string id);

        Task<Result<PermissionModel>> GetAllPermissionsAsync(string roleId);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}