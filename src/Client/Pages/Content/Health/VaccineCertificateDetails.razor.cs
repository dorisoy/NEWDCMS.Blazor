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
    public partial class VaccineCertificateDetails
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public MudTheme Theme = new MudTheme();

        private bool _loaded;
        private string qrApi = "https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=";
        protected override async Task OnInitializedAsync()
        {
            _loaded = false;
            await Task.Delay(1000);
            ImgSrc = qrApi + "http://tlssoftwarevn.com";
            FullName = "John Alex Doe";
            DoB = "01/01/1990";
            ContactNo = "0123456789";
            Address = "Empire City - Damansara - Kuala Lumpur";
            _loaded = true;
        }

        public string ImgSrc { get; set; }
        public string FullName { get; set; }
        public string DoB { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
    }
}
