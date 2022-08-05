using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class OrderDetail
    {
        private List<BreadcrumbItem> _routes = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Shopping", href: "/shopping"),
            new BreadcrumbItem("Order History", href: "/orderhistory"),
            new BreadcrumbItem("Detail", href: null, disabled: true)
        };
        protected override  Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        public MudTheme Theme = new MudTheme();

        public int IntValue { get; set; } = 1;
        public double DoubleValue { get; set; }
        public decimal DecimalValue { get; set; }

        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ValidThru { get; set; }
        public string CVV { get; set; }

        public string UserRealName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
    }
}
