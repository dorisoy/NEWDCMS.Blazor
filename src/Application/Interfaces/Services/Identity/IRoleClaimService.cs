using DCMS.Application.Interfaces.Common;
using DCMS.Application.Models.Identity;
using DCMS.Application.Requests.Identity;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimModel>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimModel>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimModel>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}