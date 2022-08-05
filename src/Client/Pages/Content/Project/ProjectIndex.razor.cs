using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Project
{
    public partial class ProjectIndex
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

        public void NavigateToHome1()
        {
            NavigationManager.NavigateTo("/project-home1");
        }
        public void NavigateToHome2()
        {
            NavigationManager.NavigateTo("/project-home2");
        }

        public void NavigateToHome3()
        {
            NavigationManager.NavigateTo("/project-home3");
        }

        public void NavigateToProject1()
        {
            NavigationManager.NavigateTo("/project-project1");
        }

        public void NavigateToProject2()
        {
            NavigationManager.NavigateTo("/project-project2");
        }

        public void NavigateToTaskCards()
        {
            NavigationManager.NavigateTo("/project-task-cards");
        }

        public void NavigateToTaskList()
        {
            NavigationManager.NavigateTo("/project-task-list");
        }

        public void NavigateToTaskCategories()
        {
            NavigationManager.NavigateTo("/project-task-categories");
        }

        public void NavigateToToDoList()
        {
            NavigationManager.NavigateTo("/project-todo-list");
        }
    }
}
