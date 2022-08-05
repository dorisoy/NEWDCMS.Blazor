using System.Linq;
using Microsoft.AspNetCore.Components;
using DCMS.Shared.Models;
using DCMS.Web.Infrastructure.Services;
using DCMS.Client.Extensions;

namespace DCMS.Client.Shared
{
	public partial class NavMenu
	{

		[Inject] IMenuService MenuService { get; set; }
		[Inject] NavigationManager NavMan { get; set; }

		string _section;
		string _componentLink;

		protected override void OnInitialized()
		{
			Refresh();
			base.OnInitialized();
		}

		public void Refresh()
		{
			_section = NavMan.GetSection();
			_componentLink = NavMan.GetMenuLink();
			StateHasChanged();
		}

		bool IsSubGroupExpanded(Menu item)
		{
			return item.GroupMenus.Any(i => i.Link == _componentLink);
		}
	}
}
