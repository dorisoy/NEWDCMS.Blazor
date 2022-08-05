using DCMS.Client.Shared.Components;
using Microsoft.AspNetCore.Components;
using DCMS.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class StoreDetailModal
    {
        //private Map _Map;
        //private string _MapsApiKey;
        [Parameter] public StoreInfo StoreInfo { get; set; } = new();
    }
}
