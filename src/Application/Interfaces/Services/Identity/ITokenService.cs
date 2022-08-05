using DCMS.Application.Interfaces.Common;
using DCMS.Application.Models.Identity;
using DCMS.Application.Requests.Identity;
using DCMS.Shared.Wrapper;
using System.Threading.Tasks;

namespace DCMS.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenModel>> LoginAsync(TokenRequest model);

        Task<Result<TokenModel>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}