using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Components
{
    public partial class ComponentsIndex
    {
        private bool _loaded;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            _loaded = true;
        }

        public void NavigateToAlertComponent()
        {
            NavigationManager.NavigateTo("/components/alert");
        }
        public void NavigateToAvatarComponent()
        {
            NavigationManager.NavigateTo("/components/avatar");
        }

        public void NavigateToBadgeComponent()
        {
            NavigationManager.NavigateTo("/components/badge");
        }

        public void NavigateToButtonsComponent()
        {
            NavigationManager.NavigateTo("/components/buttons");
        }

        public void NavigateToCardComponent()
        {
            NavigationManager.NavigateTo("/components/cards");
        }

        public void NavigateToCarouselComponent()
        {
            NavigationManager.NavigateTo("/components/carousel");
        }

        public void NavigateToChartsComponent()
        {
            NavigationManager.NavigateTo("/components/charts");
        }

        public void NavigateToChipsComponent()
        {
            NavigationManager.NavigateTo("/components/chips");
        }

        public void NavigateToDialogComponent()
        {
            NavigationManager.NavigateTo("/components/dialog");
        }
        public void NavigateToExpansionPanelComponent()
        {
            NavigationManager.NavigateTo("/components/expansion");
        }

        public void NavigateToFileUploadComponent()
        {
            NavigationManager.NavigateTo("/components/file-upload");
        }

        public void NavigateToFormInputsComponent()
        {
            NavigationManager.NavigateTo("/components/form-inputs");
        }

        public void NavigateToListComponent()
        {
            NavigationManager.NavigateTo("/components/list");
        }

        public void NavigateToPickersComponent()
        {
            NavigationManager.NavigateTo("/components/pickers");
        }

        public void NavigateToProgressComponent()
        {
            NavigationManager.NavigateTo("/components/progress");
        }

        public void NavigateToSkeletonComponent()
        {
            NavigationManager.NavigateTo("/components/skeleton");
        }

        public void NavigateToTimelineComponent()
        {
            NavigationManager.NavigateTo("/components/timeline");
        }

        public void NavigateToTypographyComponent()
        {
            NavigationManager.NavigateTo("/components/typography");
        }

    }
}
