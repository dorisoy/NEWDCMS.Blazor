using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class Cart
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Shopping", href: "/shopping"),
            new BreadcrumbItem("Cart", href: null, disabled: true)
        };
        protected override  Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        public void NavigateToCheckout()
        {
            NavigationManager.NavigateTo("/shopping/cart/checkout");
        }

        public MudTheme Theme = new MudTheme();

        public int IntValue { get; set; }
        public double DoubleValue { get; set; }
        public decimal DecimalValue { get; set; }
    }
}
