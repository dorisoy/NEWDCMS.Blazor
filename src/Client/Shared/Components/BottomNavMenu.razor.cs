using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DCMS.Client.Shared.Components
{
    public partial class BottomNavMenu : MudComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool ShowMenuTitle { get; set; } = true;
    }
}
