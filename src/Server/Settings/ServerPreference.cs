using System.Linq;
using DCMS.Shared.Constants.Localization;
using DCMS.Shared.Settings;

namespace DCMS.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}