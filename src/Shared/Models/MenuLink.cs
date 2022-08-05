using System.Collections.Generic;
using System;
using System.Linq;

namespace DCMS.Shared.Models
{
	public class MenuLink
	{
		public string Href { get; set; }
		public string Title { get; set; }
		public string Group { get; set; }
		public string GroupName { get; set; }
	}



	public class Menu
	{
		public string Name { get; set; }
		public string Title { get; set; }
		public string Link { get; set; }
		public bool IsNavGroup { get; set; }
		public bool NavGroupExpanded { get; set; }
		public List<Menu> GroupMenus { get; set; }
		public string GroupName { get; set; }
	}


	public class MainMenus
	{
		private List<Menu> _menus = new();
		public List<Menu> Menus => _menus;

		public List<Menu> GetMenusSortedByName()
		{
			return _menus.OrderByDescending(e => e.Name).ToList();
		}


		public MainMenus AddItem(string name,string title)
		{
			var componentItem = new Menu
			{
				Name = name,
				Title = title,
				Link = name.ToLowerInvariant().Replace(" ", ""),
				IsNavGroup = false
			};
			_menus.Add(componentItem);
			return this;
		}

		public MainMenus AddNavGroup(string name, string title, bool expanded, MainMenus groupItems)
		{
			var componentItem = new Menu
			{
				Name = name,
				Title = title,
				NavGroupExpanded = expanded,
				GroupMenus = groupItems.GetMenusSortedByName(),
				IsNavGroup = true
			};
			_menus.Add(componentItem);
			return this;
		}

	 
	}
}
