using DCMS.Web.Infrastructure.Services.Identity.Authentication;
using Microsoft.AspNetCore.Components;
using AntDesign;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Toolbelt.Blazor;

namespace DCMS.Web.Infrastructure.Services.Interceptors
{
    public class HttpInterceptorService : IHttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly IAuthenticationService _authenticationManager;
        private readonly NavigationManager _navigationManager;
        private readonly IMessage _snackBar;

        public HttpInterceptorService(
            HttpClientInterceptor interceptor,
            IAuthenticationService authenticationManager,
            NavigationManager navigationManager,
            IMessage snackBar)
        {
            _interceptor = interceptor;
            _authenticationManager = authenticationManager;
            _navigationManager = navigationManager;
            _snackBar = snackBar;
        }

        public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            if (!absPath.Contains("token") && !absPath.Contains("accounts"))
            {
                try
                {
                    var token = await _authenticationManager.TryRefreshToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        await _snackBar.Info("刷新Token.");
                        e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await _snackBar.Info("你已经注销.");
                    await _authenticationManager.Logout();
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
    }
}