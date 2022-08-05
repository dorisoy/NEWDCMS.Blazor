using System.Collections.Generic;
using System.Threading.Tasks;
using DCMS.Application.Interfaces.Services.Storage;
using DCMS.Server.Settings;
using DCMS.Shared.Constants.Storage;
using DCMS.Shared.Settings;
using DCMS.Shared.Wrapper;
using Microsoft.Extensions.Localization;

namespace DCMS.Server.Services.Preferences
{
    public class ServerPreferenceManager : IServerPreferenceManager
    {
        private readonly IServerStorageService _serverStorageService;
        private readonly IStringLocalizer<ServerPreferenceManager> _localizer;

        public ServerPreferenceManager(
            IServerStorageService serverStorageService,
            IStringLocalizer<ServerPreferenceManager> localizer)
        {
            _serverStorageService = serverStorageService;
            _localizer = localizer;
        }

        public async Task<IResult> ChangeLanguageAsync(string languageCode)
        {
            var preference = await GetPreference() as ServerPreference;
            if (preference != null)
            {
                preference.LanguageCode = languageCode;
                await SetPreference(preference);
                return new Result
                {
                    Succeeded = true,
                    Messages = new List<string> { _localizer["Server Language has been changed"] }
                };
            }

            return new Result
            {
                Succeeded = false,
                Messages = new List<string> { _localizer["Failed to get server preferences"] }
            };
        }

        public async Task<IPreference> GetPreference()
        {
            return await _serverStorageService.GetItemAsync<ServerPreference>(StorageConstants.Server.Preference) ?? new ServerPreference();
        }

        public async Task SetPreference(IPreference preference)
        {
            await _serverStorageService.SetItemAsync(StorageConstants.Server.Preference, preference as ServerPreference);
        }
    }
}