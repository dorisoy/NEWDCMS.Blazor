using System.Collections.Generic;
using System.Threading.Tasks;
using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using DCMS.Shared.Wrapper;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services.Identity.RoleClaims
{
    public interface IRoleClaimService : IService
    {
        Task<IResult<List<RoleClaimModel>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimModel>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}