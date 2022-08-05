using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Commerce
{
    public partial class Invoices
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private List<BreadcrumbItem> _routes = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Shopping", href: "/shopping"),
            new BreadcrumbItem("Order History", href: null, disabled: true)
        };
        protected override async Task OnInitializedAsync()
        {

        }

        private async Task NavigateToOrderDetail()
        {
            NavigationManager.NavigateTo("/orderhistory/detail");
        }

        public MudTheme Theme = new MudTheme();
    }
}
