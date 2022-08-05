using DCMS.Application.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content.Commerce
{
    public partial class Products1
    {
        public bool _loaded;
        Menu menu;
        private List<ProductItemInfo> _productItemListSelected;
        private List<ProductItemInfo> _productItemList;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            _productItemListSelected = new List<ProductItemInfo>
            {
                new ProductItemInfo
                {
                    Id = 1,
                    Image = "/images/pictures/1s.jpg",
                    ItemName = "Modern PWA",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$336.50"
                },
                new ProductItemInfo
                {
                    Id = 2,
                    Image = "/images/pictures/8s.jpg",
                    ItemName = ".NET Core Razor",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$29.99"
                },
                new ProductItemInfo
                {
                    Id = 3,
                    Image = "/images/pictures/2s.jpg",
                    ItemName = "PWA Ready Kit",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$199.00"
                },
                new ProductItemInfo
                {
                    Id = 4,
                    Image = "/images/pictures/3s.jpg",
                    ItemName = "DCMS.Blazor App",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$23.99"
                },
                new ProductItemInfo
                {
                    Id = 5,
                    Image = "/images/pictures/4s.jpg",
                    ItemName = "Blazor Hero App",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$123.45"
                },
                new ProductItemInfo
                {
                    Id = 6,
                    Image = "/images/pictures/5s.jpg",
                    ItemName = "MAUI and Xamarin",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$1,230.50"
                }
            };

            _productItemList = new List<ProductItemInfo>
            {
                new ProductItemInfo
                {
                    Id = 7,
                    Image = "/images/pictures/6s.jpg",
                    ItemName = "MAUI and Xamarin",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$35.99"
                },
                new ProductItemInfo
                {
                    Id = 8,
                    Image = "/images/pictures/7s.jpg",
                    ItemName = "Blazor Hero App",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$99.99"
                },
                new ProductItemInfo
                {
                    Id = 9,
                    Image = "/images/pictures/9s.jpg",
                    ItemName = "DCMS.Blazor App",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$150.50"
                },
                new ProductItemInfo
                {
                    Id = 10,
                    Image = "/images/pictures/10s.jpg",
                    ItemName = "PWA Ready Kit",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$99.99"
                },
                new ProductItemInfo
                {
                    Id = 11,
                    Image = "/images/pictures/11s.jpg",
                    ItemName = "MAUI and Xamarin",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$1,999.00"
                },
                new ProductItemInfo
                {
                    Id =12,
                    Image = "/images/pictures/8s.jpg",
                    ItemName = ".NET Core Razor",
                    Descriptions = "Our store guarantees the followig perks to all it's customers.",
                    Price = "$29.99"
                }
            };

            _loaded = true;
        }

        public void NavigateToProductDetail()
        {
            NavigationManager.NavigateTo("/productdetail");
        }

        public class Menu
        {
            public string Name { get; set; }
        }

        Func<Menu, string> converter = p => p?.Name;
    }
}
