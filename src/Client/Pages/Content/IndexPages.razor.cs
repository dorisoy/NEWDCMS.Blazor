using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class IndexPages
    {
        private bool _loaded;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            _loaded = true;
        }

        public void NavigateToEducationIndexPage()
        {
            NavigationManager.NavigateTo("/learning");
        }

        public void NavigateToBlogIndexPage()
        {
            NavigationManager.NavigateTo("/blogs");
        }

        public void NavigateToProjectIndexPage()
        {
            NavigationManager.NavigateTo("/project");
        }

        public void NavigateToHealthIndexPage()
        {
            NavigationManager.NavigateTo("/health");
        }

        public void NavigateToCommerceIndexPage()
        {
            NavigationManager.NavigateTo("/commerce");
        }

        public void NavigateToWalletIndexPage()
        {
            NavigationManager.NavigateTo("/wallet");
        }

        public void NavigateToComponentsIndexPage()
        {
            NavigationManager.NavigateTo("/components");
        }

        public void NavigateToCustomComponentsIndexPage()
        {
            NavigationManager.NavigateTo("/component-page");
        }
    }
}
