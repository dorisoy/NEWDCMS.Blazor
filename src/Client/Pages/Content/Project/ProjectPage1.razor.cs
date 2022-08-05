using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Project
{
    public partial class ProjectPage1
    {
        public bool _loaded;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            _loaded = true;
        }

        public bool Dense_CheckBox1 { get; set; } = false;
        public bool Dense_CheckBox2 { get; set; } = false;
        public bool Dense_CheckBox3 { get; set; } = false;
        public bool Dense_CheckBox4 { get; set; } = true;
        public bool Dense_CheckBox5 { get; set; } = true;
        public bool Dense_CheckBox6 { get; set; } = true;
        public bool Dense_CheckBox7 { get; set; } = true;
    }
}
