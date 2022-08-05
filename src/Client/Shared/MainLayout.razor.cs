using DCMS.Client.Extensions;
using DCMS.Web.Infrastructure.Settings;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace DCMS.Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        private MudTheme _currentTheme;
        private bool _rightToLeft = false;
        public bool IsFirstVisit { get; set; }

        private async Task RightToLeftToggle(bool value)
        {
            _rightToLeft = value;
            await Task.CompletedTask;
        }

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = AppTheme.DefaultTheme;
            _currentTheme = await _clientPreferenceService.GetCurrentThemeAsync();
            _rightToLeft = await _clientPreferenceService.IsRTL();
            _interceptor.RegisterEvent();

            IsFirstVisit = await _clientPreferenceService.IsFirstVisit();
        }

        public async Task DarkMode()
        {
            bool isDarkMode = await _clientPreferenceService.ToggleDarkModeAsync();
            _currentTheme = isDarkMode
                ? AppTheme.DefaultTheme
                : AppTheme.DarkTheme;

            StateHasChanged();
        }
        public async Task ToggleRTL()
        {
            var isRtl = await _clientPreferenceService.ToggleLayoutDirection();
            _rightToLeft = isRtl;

            StateHasChanged();
        }
        public void Dispose()
        {
            _interceptor.DisposeEvent();
        }
    }
}