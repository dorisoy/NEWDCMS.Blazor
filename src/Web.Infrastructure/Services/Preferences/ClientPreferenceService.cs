using System.Collections.Generic;
using Blazored.LocalStorage;
using DCMS.Web.Infrastructure.Settings;
using System.Threading.Tasks;
using DCMS.Shared.Constants.Storage;
using DCMS.Shared.Settings;
using DCMS.Shared.Wrapper;
using Microsoft.Extensions.Localization;

namespace DCMS.Web.Infrastructure.Services.Preferences
{
    /// <summary>
    /// 偏好设置
    /// </summary>
    public class ClientPreferenceService : IClientPreferenceService
    {
        private readonly ILocalStorageService _localStorageService;

        public ClientPreferenceService(
            ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<bool> ToggleDarkModeAsync()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                preference.IsDarkMode = !preference.IsDarkMode;
                await SetPreference(preference);
                return !preference.IsDarkMode;
            }

            return false;
        }
        public async Task<bool> ToggleLayoutDirection()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                preference.IsRTL = !preference.IsRTL;
                await SetPreference(preference);
                return preference.IsRTL;
            }
            return false;
        }

        public async Task<IResult> ChangeLanguageAsync(string languageCode)
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                preference.LanguageCode = languageCode;
                await SetPreference(preference);
                return new Result
                {
                    Succeeded = true,
                    Messages = new List<string> { "Client Language has been changed" }
                };
            }

            return new Result
            {
                Succeeded = false,
                Messages = new List<string> { "Failed to get client preferences" }
            };
        }

        public async Task ChangeFirstVisitAsync(bool isFirstVisit)
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                preference.IsFirstVisit = isFirstVisit;
                await SetPreference(preference);
            }
        }
        public async Task<MudTheme> GetCurrentThemeAsync()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                if (preference.IsDarkMode == true) return AppTheme.DarkTheme;
            }
            return AppTheme.DefaultTheme;
        }
        public async Task<bool> IsRTL()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                if (preference.IsDarkMode == true) return false;
            }
            return preference.IsRTL;
        }
        public async Task<string> LanguageCode()
        {
            var preference = await GetPreference() as ClientPreference;
            return preference != null ? preference.LanguageCode : "zh-CN";
        }
        public async Task<bool> IsFirstVisit()
        {
            var preference = await GetPreference() as ClientPreference;
            if (preference != null)
            {
                return preference.IsFirstVisit;
            }
            return preference.IsFirstVisit;
        }

        public async Task<IPreference> GetPreference()
        {
            return await _localStorageService.GetItemAsync<ClientPreference>(StorageConstants.Local.Preference) ?? new ClientPreference();
        }

        public async Task SetPreference(IPreference preference)
        {
            await _localStorageService.SetItemAsync(StorageConstants.Local.Preference, preference as ClientPreference);
        }
    }
}