using DCMS.Shared.Services;
using MudBlazor;
using System.Threading.Tasks;

namespace DCMS.Web.Infrastructure.Services.Preferences
{
    public interface IClientPreferenceService : IPreferenceService
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
        Task ChangeFirstVisitAsync(bool isFirstVisit);
    }
}