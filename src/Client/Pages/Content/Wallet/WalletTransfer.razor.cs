using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Wallet
{
    public partial class WalletTransfer
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        //private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        //{
        //    new BreadcrumbItem("Pages", href: "/pages"),
        //     new BreadcrumbItem("Wallet", href: "/pages/wallet"),
        //    new BreadcrumbItem("Transfer", href: null, disabled: true)
        //};
        protected override  Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        private async Task NavigateToTransactionDetail()
        {
            NavigationManager.NavigateTo("/wallet/transactiondetail");
        }

        public MudTheme Theme = new MudTheme();

        public int IntValue { get; set; } = 1;
        public double DoubleValue { get; set; }
        public decimal DecimalValue { get; set; }


        public string AccHolderName { get; set; }
        public string AccNumber { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ValidThru { get; set; }
        public string CVV { get; set; }

        public string UserRealName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
    }
}
