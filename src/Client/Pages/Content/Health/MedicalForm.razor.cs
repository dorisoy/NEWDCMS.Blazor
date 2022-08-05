using DCMS.Web.Infrastructure.Settings;
using DCMS.Client.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Health
{
    public partial class MedicalForm
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public MudTheme Theme = new MudTheme();
        protected override  Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        public string FullName { get; set; }
        public string IDNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }
}
