using DCMS.Application.Interfaces.Common;
using DCMS.Application.Models.Identity;
using DCMS.Application.Requests.Identity;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.Application.Interfaces.Services.Identity
{
    public interface IUserService : IService
    {
        Task<Result<List<UserModel>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<IResult<UserModel>> GetAsync(string userId);

        Task<IResult> RegisterAsync(RegisterRequest request, string origin);

        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);

        Task<IResult<UserRolesModel>> GetRolesAsync(string id);

        Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);

        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);

        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);

        Task<string> ExportToExcelAsync(string searchString = "");
    }
}