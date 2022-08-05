using System.Threading.Tasks;
using Toolbelt.Blazor;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services.Interceptors
{
    public interface IHttpInterceptorService : IService
    {
        void RegisterEvent();

        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        void DisposeEvent();
    }
}