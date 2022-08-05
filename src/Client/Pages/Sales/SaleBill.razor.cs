using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
//using DCMS.Web.Infrastructure.Services.Dashboard;
//using DCMS.Shared.Constants.Application;
//using DCMS.Client.Extensions;
using DCMS.Application.Models.Sale;
using System.Linq;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace DCMS.Client.Pages.Sales
{
	public partial class SaleBill
	{
		private bool _loaded;
		private bool _isBusy;

		private List<SaleReservationBillModel> _bills = new();
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
				var result = JsonConvert.DeserializeObject<List<SaleReservationBillModel>>(json);
				if (result.Any())
				{
					_bills = result;
					_isBusy = false;
				}
				else
				{
					_snackBar.Add("数据加载失败！", Severity.Error);
				}
			}
			catch (Exception ex)
			{
				_snackBar.Add(ex.Message, Severity.Error);
			}
		}


		private void ShowBtnPress(int nr)
		{
			var temp = _bills.First(f => f.Id == nr);
			temp.IsArchive = !temp.IsArchive;
		}

	}
}

