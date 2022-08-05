using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class OrderHistory
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private List<BreadcrumbItem> _routes = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Shopping", href: "/shopping"),
            new BreadcrumbItem("Order History", href: null, disabled: true)
        };
        protected override Task OnInitializedAsync()
		{
			return Task.CompletedTask;
		}

		private void NavigateToOrderDetail()
		{
			NavigationManager.NavigateTo("/orderhistory/detail");
		}

		public MudTheme Theme = new MudTheme();
    }
}
