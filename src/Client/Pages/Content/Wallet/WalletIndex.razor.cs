using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Wallet
{
    public partial class WalletIndex
    {
        private bool _loaded;
        private List<PostSummary> _postList;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            _loaded = true;
        }

        public void NavigateToWalletDashboard()
        {
            NavigationManager.NavigateTo("/dashboard-wallet");
        }
        public void NavigateToTransactions()
        {
            NavigationManager.NavigateTo("/wallet/transfer");
        }

        public void NavigateToTransactionDetail()
        {
            NavigationManager.NavigateTo("/wallet/transactiondetai");
        }

        public void NavigateToWalletCards()
        {
            NavigationManager.NavigateTo("/wallet-cards");
        }

        public void NavigateToWalletAccount()
        {
            NavigationManager.NavigateTo("/wallet-account");
        }

        public void NavigateToPromotionsPage()
        {
            NavigationManager.NavigateTo("/promotions");
        }

        public void NavigateToWalletSettings()
        {
            NavigationManager.NavigateTo("/wallet-settings");
        }
    }
}
