using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class ProductDetail
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Shopping", href: "/shopping"),
            new BreadcrumbItem("Product Details", href: null, disabled: true)
        };


        public void NavigateToCheckout()
        {
            NavigationManager.NavigateTo("/shopping/cart/checkout");
        }

        public void NavigateToCartDetail()
        {
            NavigationManager.NavigateTo("/shopping/cart");
        }
    }
}
