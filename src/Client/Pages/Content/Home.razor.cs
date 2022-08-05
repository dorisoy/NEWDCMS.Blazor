using DCMS.Web.Infrastructure.Settings;
using DCMS.Client.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public MudTheme Theme = new MudTheme();
        protected override  Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        private void InvokeQrPinModal()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, FullScreen = true, DisableBackdropClick = true };
            _dialogService.Show<QrPinModal>("", parameters, options);
        }


        public void NavigateToDashboardPage()
        {
            NavigationManager.NavigateTo("/dashboard");
        }
    }
}
