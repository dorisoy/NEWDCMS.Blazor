using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Wallet
{
    public partial class WalletAccount
    {
        public bool _loaded = false;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            _loaded = true;
        }

        public void NavigateToCommercePage()
        {
            NavigationManager.NavigateTo("/dashboard-commerce");
        }

        public void NavigateToWalletDashboard()
        {
            NavigationManager.NavigateTo("/dashboard-wallet");
        }
    }
}
