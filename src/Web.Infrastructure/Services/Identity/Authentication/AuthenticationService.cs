using Blazored.LocalStorage;
using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using DCMS.Web.Infrastructure.Authentication;
using DCMS.Web.Infrastructure.Extensions;
using DCMS.Web.Infrastructure.Routes;
using DCMS.Shared.Constants.Storage;
using DCMS.Shared.Wrapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DCMS.Web.Infrastructure.Services.Identity.Authentication
{

    /// <summary>
    /// 身份认证服务
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;


        public AuthenticationService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ClaimsPrincipal> CurrentUser()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return state.User;
        }

        public async Task<IResult> Login(TokenRequest model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.Get, model);
                var result = await response.ToResult<TokenModel>();
                if (result.Succeeded)
                {
                    var token = result.Data.Token;
                    var refreshToken = result.Data.RefreshToken;
                    var userImageURL = result.Data.UserImageURL;
                    await _localStorage.SetItemAsync(StorageConstants.Local.AuthToken, token);
                    await _localStorage.SetItemAsync(StorageConstants.Local.RefreshToken, refreshToken);
                    if (!string.IsNullOrEmpty(userImageURL))
                    {
                        await _localStorage.SetItemAsync(StorageConstants.Local.UserImageURL, userImageURL);
                    }

                    await ((AppStateProvider)this._authenticationStateProvider).StateChangedAsync();

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    return await Result.SuccessAsync();
                }
                else
                {
                    return await Result.FailAsync(result.Messages);
                }
            }
            catch (Exception ex)
            {
                return await Result.FailAsync(ex.Message);
            }
        }

        public async Task<IResult> Logout()
        {
            await _localStorage.RemoveItemAsync(StorageConstants.Local.AuthToken);
            await _localStorage.RemoveItemAsync(StorageConstants.Local.RefreshToken);
            await _localStorage.RemoveItemAsync(StorageConstants.Local.UserImageURL);
            ((AppStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return await Result.SuccessAsync();
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);
            var refreshToken = await _localStorage.GetItemAsync<string>(StorageConstants.Local.RefreshToken);

            var response = await _httpClient.PostAsJsonAsync(Routes.TokenEndpoints.Refresh, new RefreshTokenRequest { Token = token, RefreshToken = refreshToken });

            var result = await response.ToResult<TokenModel>();

            if (!result.Succeeded)
            {
                throw new ApplicationException("Something went wrong during the refresh token action");
            }

            token = result.Data.Token;
            refreshToken = result.Data.RefreshToken;
            await _localStorage.SetItemAsync(StorageConstants.Local.AuthToken, token);
            await _localStorage.SetItemAsync(StorageConstants.Local.RefreshToken, refreshToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return token;
        }

        public async Task<string> TryRefreshToken()
        {
            //check if token exists
            var availableToken = await _localStorage.GetItemAsync<string>(StorageConstants.Local.RefreshToken);
            if (string.IsNullOrEmpty(availableToken)) return string.Empty;
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var exp = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var timeUTC = DateTime.UtcNow;
            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 1)
                return await RefreshToken();
            return string.Empty;
        }

        public async Task<string> TryForceRefreshToken()
        {
            return await RefreshToken();
        }
    }
}