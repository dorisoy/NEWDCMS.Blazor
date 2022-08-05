using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using DCMS.Web.Infrastructure.Services.Dashboard;
using DCMS.Shared.Constants.Application;
using DCMS.Client.Extensions;

namespace DCMS.Client.Pages.Content
{
    public partial class Dashboard
    {
        [Inject] private IDashboardService DashboardService { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [Parameter] public int ProductCount { get; set; }
        [Parameter] public int BrandCount { get; set; }
        [Parameter] public int DocumentCount { get; set; }
        [Parameter] public int DocumentTypeCount { get; set; }
        [Parameter] public int DocumentExtendedAttributeCount { get; set; }
        [Parameter] public int UserCount { get; set; }
        [Parameter] public int RoleCount { get; set; }

        private readonly string[] _dataEnterBarChartXAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private readonly List<ChartSeries> _dataEnterBarChartSeries = new();
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_NavigationManager, _localStorage);
            HubConnection.On(ApplicationConstants.SignalR.ReceiveUpdateDashboard, async () =>
            {
                await LoadDataAsync();
                StateHasChanged();
            });
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            var Model = await DashboardService.GetDataAsync();
            if (Model.Succeeded)
            {
                ProductCount = Model.Data.ProductCount;
                BrandCount = Model.Data.BrandCount;
                DocumentCount = Model.Data.DocumentCount;
                DocumentTypeCount = Model.Data.DocumentTypeCount;
                DocumentExtendedAttributeCount = Model.Data.DocumentExtendedAttributeCount;
                UserCount = Model.Data.UserCount;
                RoleCount = Model.Data.RoleCount;
                foreach (var item in Model.Data.DataEnterBarChart)
                {
                    _dataEnterBarChartSeries
                        .RemoveAll(x => x.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                    _dataEnterBarChartSeries.Add(new ChartSeries { Name = item.Name, Data = item.Data });
                }
            }
            else
            {
                foreach (var message in Model.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }


        private void TestAsync1()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.Dialog.ContentText),"测试1！"}
            };
            _dialogService.Show<Shared.Dialogs.Dialog>("", parameters, options);
        }

        private void TestAsync2()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.Dialog.ContentText),"测试2！"}
            };
            _dialogService.Show<Shared.Dialogs.Dialog>("", parameters, options);
        }

        private void TestAsync3()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.Dialog.ContentText),"测试3！"}
            };
            _dialogService.Show<Shared.Dialogs.Dialog>("", parameters, options);
        }



    }
}