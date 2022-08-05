using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace DCMS.Client.Infrastructure.Extensions
{
    /// <summary>
    /// 导航扩展
    /// </summary>
    public static class NavigationManagerExtension
    {
        public static string QueryString(this NavigationManager nav, string paramName)
        {
            var uri = nav.ToAbsoluteUri(nav.Uri);
            string paramValue = HttpUtility.ParseQueryString(uri.Query).Get(paramName);
            return paramValue ?? "";
        }
    }
}
