using System.Linq;
using DCMS.Shared.Constants.Localization;
using DCMS.Shared.Settings;

namespace DCMS.Web.Infrastructure.Settings
{
    public record ClientPreference : IPreference
    {
        public bool IsDarkMode { get; set; }
        public bool IsRTL { get; set; }
        public bool IsDrawerOpen { get; set; }
        public string PrimaryColor { get; set; }
        public bool IsFirstVisit { get; set; } = true;
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "zh-CN";
    }
}