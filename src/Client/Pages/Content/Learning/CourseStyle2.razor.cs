using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Learning
{
    public partial class CourseStyle2
    {
        public bool _loaded;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            _loaded = true;
        }

        public void NavigateToCommerceDashboard()
        {
            NavigationManager.NavigateTo("/dashboard-commerce");
        }

        public void NavigateToCategoriesPage()
        {
            NavigationManager.NavigateTo("/categories");
        }

        public void NavigateToProductPage1()
        {
            NavigationManager.NavigateTo("/products-1");
        }

        public void NavigateToProductPage2()
        {
            NavigationManager.NavigateTo("/products-2");
        }

        public void NavigateToProductDetailPage()
        {
            NavigationManager.NavigateTo("/productdetail");
        }

        public void NavigateToCartPage()
        {
            NavigationManager.NavigateTo("/cart");
        }

        public void NavigateToCheckoutPage()
        {
            NavigationManager.NavigateTo("/checkout");
        }

        public void NavigateToInvoicesPage()
        {
            NavigationManager.NavigateTo("/invoices");
        }

        public void NavigateToInvoicePage()
        {
            NavigationManager.NavigateTo("/invoice");
        }
    }
}
