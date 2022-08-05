using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using DCMS.Application.Models.Sale;
using System.Linq;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace DCMS.Client.Pages.Sales
{
    public partial class SaleReservationBillAdd
    {
        private bool _loaded;
        private bool _isBusy;

        
        private SaleReservationBillModel _bill = new();




        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            _loaded = true;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                _isBusy = true;
                var json = await httpClient.GetStringAsync("data/order.json");
                var result = new List<SaleReservationItemModel>();
                if (result.Any())
                {
                    _items = result;
                    _isBusy = false;
                }
                else
                {
                    _snackBar.Add(" ˝æ›º”‘ÿ ß∞‹£°", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                _snackBar.Add(ex.Message, Severity.Error);
            }
        }



        private List<string> editEvents = new();
        private bool dense = false;
        private bool hover = true;
        private bool ronly = false;
        private bool canCancelEdit = false;
        private bool blockSwitch = false;
        private string searchString = "";
        private SaleReservationItemModel selectedItem1 = null;
        private SaleReservationItemModel elementBeforeEdit;
        private HashSet<SaleReservationItemModel> selectedItems1 = new HashSet<SaleReservationItemModel>();
        private MudTable<SaleReservationItemModel> _table;
        private List<SaleReservationItemModel> _items = new();


        private void ClearEventLog()
        {
            editEvents.Clear();
        }

        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private void BackupItem(object element)
        {
            elementBeforeEdit = new()
            {
                Id = ((SaleReservationItemModel)element).Id,
                BillId = ((SaleReservationItemModel)element).BillId,
                ProductId = ((SaleReservationItemModel)element).ProductId,
                Quantity = ((SaleReservationItemModel)element).Quantity,
                Remark = ((SaleReservationItemModel)element).Remark
            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((SaleReservationItemModel)element).Id}");
        }

        private void ItemHasBeenCommitted(object element)
        {
            AddEditionEvent($"RowEditCommit event: Changes to Element {((SaleReservationItemModel)element).Id} committed");
        }

        private void ResetItemToOriginalValues(object element)
        {
            ((SaleReservationItemModel)element).Id = elementBeforeEdit.Id;
            ((SaleReservationItemModel)element).BillId = elementBeforeEdit.BillId;
            ((SaleReservationItemModel)element).ProductId = elementBeforeEdit.ProductId;
            ((SaleReservationItemModel)element).Quantity = elementBeforeEdit.Quantity;
            ((SaleReservationItemModel)element).Remark = elementBeforeEdit.Remark;
            AddEditionEvent($"RowEditCancel event: Editing of Element {((SaleReservationItemModel)element).Id} cancelled");
        }

        private bool FilterFunc(SaleReservationItemModel element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.BillId.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ProductId.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.Remark} {element.BillId} {element.ProductId}".Contains(searchString))
                return true;
            return false;
        }

        private async Task NewRow()
        {
            Console.WriteLine("oninit");
            var newRow = new SaleReservationItemModel();
            _items.Add(newRow);
            await Task.Delay(25);
            //_table.SetSelectedItem(newRow);
            //_table.SetEditingItem(newRow);
        }
    }
}

