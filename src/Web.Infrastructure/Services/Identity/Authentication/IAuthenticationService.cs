using DCMS.Application.Requests.Identity;
using DCMS.Shared.Wrapper;
using System.Security.Claims;
using System.Threading.Tasks;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services.Identity.Authentication
{
    public interface IAuthenticationService : IService
    {
        Task<IResult> Login(TokenRequest model);

        Task<IResult> Logout();

        Task<string> RefreshToken();

        Task<string> TryRefreshToken();

        Task<string> TryForceRefreshToken();

        Task<ClaimsPrincipal> CurrentUser();
    }
}