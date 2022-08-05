using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Health
{
    public partial class HealthIndex
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

        public void NavigateToHealthDashboard()
        {
            NavigationManager.NavigateTo("/dashboard-health");
        }
        public void NavigateToMedicalForm()
        {
            NavigationManager.NavigateTo("/medical-form");
        }

        public void NavigateToVaccineCertificate()
        {
            NavigationManager.NavigateTo("/vaccine-certificate");
        }

        public void NavigateToVaccineCertificateDetails()
        {
            NavigationManager.NavigateTo("/certificate-details");
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
