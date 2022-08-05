using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;
using MudBlazor.Interfaces;
using MudBlazor.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DCMS.Client.Shared.Components
{
    public partial class BottomNavMenuItem : MudBaseSelectItem
    {
        protected string Classname =>
        new CssBuilder("mud-nav-item")
          .AddClass($"mud-ripple", !DisableRipple && !Disabled)
          .AddClass(Class)
          .Build();

        protected string LinkClassname =>
        new CssBuilder("mud-nav-link")
          .AddClass($"mud-nav-link-disabled", Disabled)
          .Build();

        protected string IconClassname =>
        new CssBuilder("mud-nav-link-icon")
          .AddClass($"mud-nav-link-icon-default", IconColor == Color.Default)
          .Build();

        private Dictionary<string, object> Attributes
        {
            get => Disabled ? null : new Dictionary<string, object>()
            {
                { "href", Href }
            };
        }

        /// <summary>
        /// Icon to use if set.
        /// </summary>
        [Parameter] public string Icon { get; set; }

        /// <summary>
        /// The color of the icon. It supports the theme colors, default value uses the themes drawer icon color.
        /// </summary>
        [Parameter] public Color IconColor { get; set; } = Color.Error;

        [Parameter] public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;

        [Parameter] public string Target { get; set; }

        [Parameter] public string Title { get; set; }

        [CascadingParameter] INavigationEventReceiver NavigationEventReceiver { get; set; }
        [CascadingParameter] BottomNavMenu Parent { get; set; }

        private Task HandleNavigation()
        {
            if (!Disabled && NavigationEventReceiver != null)
            {
                return NavigationEventReceiver.OnNavigation();
            }
            return Task.CompletedTask;
        }
    }
}
