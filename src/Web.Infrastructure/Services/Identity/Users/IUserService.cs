using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services.Identity.Users
{
    public interface IUserService : IService
    {
        Task<IResult<List<UserModel>>> GetAllAsync();

        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);

        Task<IResult<UserModel>> GetAsync(string userId);

        Task<IResult<UserRolesModel>> GetRolesAsync(string userId);

        Task<IResult> RegisterUserAsync(RegisterRequest request);

        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);

        Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);

        Task<string> ExportToExcelAsync(string searchString = "");
    }
}