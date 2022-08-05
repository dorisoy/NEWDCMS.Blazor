using Microsoft.AspNetCore.Components;

namespace DCMS.Web.Pages.Dashboard.Home
{
    public partial class Trend
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Flag { get; set; }
    }
}