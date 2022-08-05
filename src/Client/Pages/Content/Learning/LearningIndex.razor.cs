using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Learning
{
    public partial class LearningIndex
    {
        private bool _loaded;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(300);
            _loaded = true;
        }

        public void NavigateToHome1()
        {
            NavigationManager.NavigateTo("/learning-home1");
        }
        public void NavigateToHome2()
        {
            NavigationManager.NavigateTo("/learning-home2");
        }

        public void NavigateToHome3()
        {
            NavigationManager.NavigateTo("/learning-home3");
        }

        public void NavigateToCategories1()
        {
            NavigationManager.NavigateTo("/learning-categories1");
        }

        public void NavigateToCategories2()
        {
            NavigationManager.NavigateTo("/learning-categories2");
        }

        public void NavigateToCourseStyle1()
        {
            NavigationManager.NavigateTo("/learning-style1");
        }

        public void NavigateToCourseStyle2()
        {
            NavigationManager.NavigateTo("/learning-style2");
        }

        public void NavigateToCourseStyle3()
        {
            NavigationManager.NavigateTo("/learning-style3");
        }

        public void NavigateToCourseProfile()
        {
            NavigationManager.NavigateTo("/learning-profile");
        }
    }
}
