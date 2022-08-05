using DCMS.Shared.Settings;
using System.Threading.Tasks;
using DCMS.Shared.Wrapper;

namespace DCMS.Shared.Services
{
    public interface IPreferenceService 
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}