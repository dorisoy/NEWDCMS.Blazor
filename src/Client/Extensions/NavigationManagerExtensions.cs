using System;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace DCMS.Client.Extensions
{
    internal static class NavigationManagerExtensions
    {

        public static string GetSection(this NavigationManager navMan)
        {
            var currentUri = navMan.Uri.Remove(0, navMan.BaseUri.Length - 1);
            var firstElement = currentUri
                .Split("/", StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault();
            return firstElement;
        }

        public static string GetMenuLink(this NavigationManager navMan)
        {
            var currentUri = navMan.Uri.Remove(0, navMan.BaseUri.Length - 1);
            var secondElement = currentUri
                .Split("/", StringSplitOptions.RemoveEmptyEntries)
                .ElementAtOrDefault(1);
            return secondElement;
        }


        public static bool IsHomePage(this NavigationManager navMan)
        {
            return navMan.Uri == navMan.BaseUri;
        }
    }
}
