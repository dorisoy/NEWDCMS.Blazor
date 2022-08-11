using AntDesign;
using AntDesign.TableModels;
using DCMS.Application.Models;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;


namespace DCMS.Web.Pages.Sales.SaleBill
{
    public partial class Index
    {
        AntDesign.Internal.IForm _form;
        private bool _loading = true;

        private List<SaleReservationBillModel> _bills = new();
        private SaleReservationBillModel _bill = new();
        IEnumerable<SaleReservationBillModel> selectedRows;

        int _pageIndex = 0;
        int _pageSize = 10;
        int _total = 0;
        ITable table;

        string size = "middle";

        private List<string> autoCompleteOptions = new List<string> { "小李", "小李", "小李", "小李", "小李", "小李" };

        record Person(int Id, string Name);
        private List<Person> _persons = new List<Person>
        {
            new Person(1,"小李"),
            new Person(2,"小李"),
            new Person(3,"小李"),
            new Person(4,"小李"),
        };



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                await LoadData(null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        async Task OnRowExpand(RowData<SaleReservationBillModel> rowData)
        {
            if (rowData.Data.Items != null)
            {
                return;
            }

            await Task.Delay(1000);


            rowData.Data.Items = Enumerable.Range(0, 3).Select(i => new SaleReservationItemModel()
            {
                Id = i,
                CreatedOnUtc = DateTime.Parse("2014-12-24 23:12:00"),
                ProductId = 343434,
                ProductName = "勇闯天涯500ML",
                BarCode = "343432432432432",
                UnitName = "箱",
                UnitConvert = "1箱=12瓶",
                Amount = Convert.ToDecimal(45.8 * 10),
                Price = Convert.ToDecimal(45.8),
                Remark = "吴老板",
                Quantity = 10
            }).ToList();

            StateHasChanged();
        }

        /// <summary>
        /// 加载单据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task LoadData(QueryModel<SaleReservationBillModel>? query)
        {

            try
            {
                _loading = true;

                var url = "data/order.json?";
                if (query != null)
                    url = "data/order.json?" + GetRandomuserParams(query);


                var json = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<List<SaleReservationBillModel>>(json);
                if (result?.Any() ?? false)
                {
                    _bills = result;
                    _loading = false;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }



        public void OnChange(QueryModel<SaleReservationBillModel> queryModel)
        {
            Console.WriteLine(JsonConvert.SerializeObject(queryModel));
            //var query = queryModel;
            //await LoadData(query);
            //_total = _bills.Count(); 
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        private string GetRandomuserParams(QueryModel<SaleReservationBillModel> queryModel)
        {
            var query = new List<string>()
            {
                $"results={queryModel.PageSize}",
                $"page={queryModel.PageIndex}",
            };

            queryModel.SortModel.ForEach(x =>
            {
                query.Add($"sortField={x.FieldName.ToLower()}");
                query.Add($"sortOrder={x.Sort}");
            });

            queryModel.FilterModel.ForEach(filter =>
            {
                filter.SelectedValues.ForEach(value =>
                {
                    query.Add($"{filter.FieldName.ToLower()}={value}");
                });
            });

            return string.Join('&', query);
        }

        /// <summary>
        /// 移除选择项
        /// </summary>
        /// <param name="id"></param>
        public void RemoveSelection(int id)
        {
            var selected = selectedRows.Where(x => x.Id != id);
            selectedRows = selected;
        }


        /// <summary>
        /// 添加单据
        /// </summary>
        private async Task Add()
        {
            var options = new ConfirmOptions()
            {
                Title = "添加单据明细",
                Style = "top:20px;",
                Width = 1300,
                IsFullScreen = true,
                OkText = "保存单据",
                CancelText = "取消"
            };

            var confirmRef = await _modalService.CreateConfirmAsync<ItemTemplate, string, string>(options, "test");

            confirmRef.OnOpen = () =>
            {
                Console.WriteLine("Open Confirm");
                return Task.CompletedTask;
            };

            confirmRef.OnClose = () =>
            {
                Console.WriteLine("Close Confirm");
                return Task.CompletedTask;
            };

            confirmRef.OnOk = (result) =>
            {
                Console.WriteLine($"OnOk:{result}");
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="id"></param>
        private async void Print()
        {
            await _message.Success("打印成功！");
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="id"></param>
        private async void Export(MouseEventArgs arg)
        {
            await _message.Success("导出成功！");
        }


        /// <summary>
        /// 单项删除
        /// </summary>
        /// <param name="id"></param>
        private async void Delete(int id)
        {
            await _message.Success("删除成功！");
        }

        /// <summary>
        /// 删除全部
        /// </summary>
        private void DeleteAll()
        {
            try
            {
                var ms = _modalService;

                _modalService.Confirm(new ConfirmOptions()
                {
                    Title = "你确定要全部删除吗?",
                    Icon = ConfirmIconRenderFragments.GetByConfirmIcon(ConfirmIcon.Info),
                    OnOk = onOk,
                    OnCancel = onCancel,
                    OkType = "danger",
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task onOk(ModalClosingEventArgs e)
        {
            await _message.Success("操作成功！");
        }


        /// <summary>
        /// 取消
        /// </summary>
        Func<ModalClosingEventArgs, Task> onCancel = (e) =>
        {
            return Task.CompletedTask;
        };


    }
}
