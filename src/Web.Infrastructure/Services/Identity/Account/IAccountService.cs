using DCMS.Application.Requests.Identity;
using DCMS.Shared.Wrapper;
using System.Threading.Tasks;
using DCMS.Shared.Services;


namespace DCMS.Web.Infrastructure.Services.Identity.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);

        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}