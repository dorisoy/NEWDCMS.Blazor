using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Commerce
{
    public partial class Categories
    {
        public bool _loaded;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            _loaded = true;
        }

        public void NavigateToCommerceIndexPage()
        {
            NavigationManager.NavigateTo("/commerce");
        }

        public void NavigateToWalletIndexPage()
        {
            NavigationManager.NavigateTo("/wallet");
        }
    }
}
