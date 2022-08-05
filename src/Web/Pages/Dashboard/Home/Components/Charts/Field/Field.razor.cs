using Microsoft.AspNetCore.Components;

namespace DCMS.Web.Pages.Dashboard.Home
{
    public partial class Field
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Value { get; set; }
    }
}