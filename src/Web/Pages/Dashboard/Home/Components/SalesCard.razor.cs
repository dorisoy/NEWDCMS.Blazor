using System.Threading.Tasks;
using AntDesign.Charts;
using DCMS.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Components;

namespace DCMS.Web.Pages.Dashboard.Home
{
	public partial class SalesCard
	{
		private readonly ColumnConfig _saleChartConfig = new ColumnConfig
		{
			Title = new AntDesign.Charts.Title
			{
				Visible = true,
				Text = "Stores Sales Trend"
			},
			ForceFit = true,
			Padding = "auto",
			XField = "x",
			YField = "y"
		};

		private readonly ColumnConfig _visitChartConfig = new ColumnConfig
		{
			Title = new AntDesign.Charts.Title
			{
				Visible = true,
				Text = "Visits Trend"
			},
			ForceFit = true,
			Padding = "auto",
			XField = "x",
			YField = "y"
		};

		private IChartComponent _saleChart;
		private IChartComponent _visitChart;

		[Parameter]
		public SaleItem[] Items { get; set; } =
		{
			new SaleItem {Id = 1, Title = "Gongzhuan No.0 shop", Total = "323,234"},
			new SaleItem {Id = 2, Title = "Gongzhuan No.1 shop", Total = "323,234"},
			new SaleItem {Id = 3, Title = "Gongzhuan No.2 shop", Total = "323,234"},
			new SaleItem {Id = 4, Title = "Gongzhuan No.3 shop", Total = "323,234"},
			new SaleItem {Id = 5, Title = "Gongzhuan No.4 shop", Total = "323,234"},
			new SaleItem {Id = 6, Title = "Gongzhuan No.5 shop", Total = "323,234"},
			new SaleItem {Id = 7, Title = "Gongzhuan No.6 shop", Total = "323,234"}
		};

		[Inject] public IChartService ChartService { get; set; }

		protected override async Task OnInitializedAsync()
		{

			await OnTabChanged("1");

			for (int i = 1; i < 50; i++)
			{
				Random rd = new Random();
				data3[i - 1] = new { type = $"分类 {i}", value = rd.Next(0, 10) + 1 };
			}

			await base.OnInitializedAsync();
		}

		private async Task OnTabChanged(string activeKey)
		{

			try
			{
				var data = await ChartService.GetSalesDataAsync();
				if (activeKey == "1")
					await _saleChart.ChangeData(data);
				else
					await _visitChart.ChangeData(data);

			}
			catch (Exception ex)
			{
				Serilog.Log.Warning($"OnTabChanged:{ex.Message}");
			}
		}


		object[] data3 = new object[49];

		#region 示例1

		object[] linedata1 = new object[] {
		new  {
	date= "2018/8/1",
	type= "download",
	value= 4623,
  },
		new  {
	date= "2018/8/1",
	type= "register",
	value= 2208,
  },
		new  {
	date= "2018/8/1",
	type= "bill",
	value= 182,
  },
		new  {
	date= "2018/8/2",
	type= "download",
	value= 6145,
  },
		new  {
	date= "2018/8/2",
	type= "register",
	value= 2016,
  },
		new  {
	date= "2018/8/2",
	type= "bill",
	value= 257,
  },
		new  {
	date= "2018/8/3",
	type= "download",
	value= 508,
  },
		new  {
	date= "2018/8/3",
	type= "register",
	value= 2916,
  },
		new  {
	date= "2018/8/3",
	type= "bill",
	value= 289,
  },
		new  {
	date= "2018/8/4",
	type= "download",
	value= 6268,
  },
		new  {
	date= "2018/8/4",
	type= "register",
	value= 4512,
  },
		new  {
	date= "2018/8/4",
	type= "bill",
	value= 428,
  },
		new  {
	date= "2018/8/5",
	type= "download",
	value= 6411,
  },
		new  {
	date= "2018/8/5",
	type= "register",
	value= 8281,
  },
		new  {
	date= "2018/8/5",
	type= "bill",
	value= 619,
  },
		new  {
	date= "2018/8/6",
	type= "download",
	value= 1890,
  },
		new  {
	date= "2018/8/6",
	type= "register",
	value= 2008,
  },
		new  {
	date= "2018/8/6",
	type= "bill",
	value= 87,
  },
		new  {
	date= "2018/8/7",
	type= "download",
	value= 4251,
  },
		new  {
	date= "2018/8/7",
	type= "register",
	value= 1963,
  },
		new  {
	date= "2018/8/7",
	type= "bill",
	value= 706,
  },
		new  {
	date= "2018/8/8",
	type= "download",
	value= 2978,
  },
		new  {
	date= "2018/8/8",
	type= "register",
	value= 2367,
  },
		new  {
	date= "2018/8/8",
	type= "bill",
	value= 387,
  },
		new  {
	date= "2018/8/9",
	type= "download",
	value= 3880,
  },
		new  {
	date= "2018/8/9",
	type= "register",
	value= 2956,
  },
		new  {
	date= "2018/8/9",
	type= "bill",
	value= 488,
  },
		new  {
	date= "2018/8/10",
	type= "download",
	value= 3606,
  },
		new  {
	date= "2018/8/10",
	type= "register",
	value= 678,
  },
		new  {
	date= "2018/8/10",
	type= "bill",
	value= 507,
  },
		new  {
	date= "2018/8/11",
	type= "download",
	value= 4311,
  },
		new  {
	date= "2018/8/11",
	type= "register",
	value= 3188,
  },
		new  {
	date= "2018/8/11",
	type= "bill",
	value= 548,
  },
		new  {
	date= "2018/8/12",
	type= "download",
	value= 4116,
  },
		new  {
	date= "2018/8/12",
	type= "register",
	value= 3491,
  },
		new  {
	date= "2018/8/12",
	type= "bill",
	value= 456,
  },
		new  {
	date= "2018/8/13",
	type= "download",
	value= 6419,
  },
		new  {
	date= "2018/8/13",
	type= "register",
	value= 2852,
  },
		new  {
	date= "2018/8/13",
	type= "bill",
	value= 689,
  },
		new  {
	date= "2018/8/14",
	type= "download",
	value= 1643,
  },
		new  {
	date= "2018/8/14",
	type= "register",
	value= 4788,
  },
		new  {
	date= "2018/8/14",
	type= "bill",
	value= 280,
  },
		new  {
	date= "2018/8/15",
	type= "download",
	value= 445,
  },
		new  {
	date= "2018/8/15",
	type= "register",
	value= 4319,
  },
		new  {
	date= "2018/8/15",
	type= "bill",
	value= 176,
  },
};

		LineConfig lineconfig1 = new LineConfig()
		{
			Title = new Title()
			{
				Visible = true,
				Text = "多折线图",
			},
			Description = new Description()
			{
				Visible = true,
				Text = "将数据按照某一字段进行分组，用于比对不同类型数据的趋势。",
			},
			Padding = "auto",
			ForceFit = true,
			XField = "date",
			YField = "value",
			YAxis = new ValueAxis()
			{
				Label = new BaseAxisLabel()
				{
					// 数值格式化为千分位
				},
			},
			Legend = new Legend()
			{
				Position = "right-top",
			},
			SeriesField = "type"
		};


		#endregion 示例1




		#region Example 1

		readonly object[] data1 =
	{
		new
		{
			type = "Category One",
			value = 27
		},
		new
		{
			type = "Category Two",
			value = 25
		},
		new
		{
			type = "Category Three",
			value = 18
		},
		new
		{
			type = "Category Four",
			value = 15
		},
		new
		{
			type = "Category Five",
			value = 10
		},
		new
		{
			type = "Other",
			value = 5
		}
	};

		readonly PieConfig config1 = new PieConfig
		{
			ForceFit = true,
			Title = new Title
			{
				Visible = true,
				Text = "品类分布情况"
			},
			Description = new Description
			{
				Visible = true,
				Text = ""
			},
			Radius = 0.8,
			AngleField = "value",
			ColorField = "type",
			Label = new PieLabelConfig
			{
				Visible = true,
				Type = "inner"
			}
		};

		#endregion Example 1

		#region Example 2

		readonly object[] data2 =
		{
		new
		{
			type = "Category One",
			value = 27
		},
		new
		{
			type = "Category Two",
			value = 25
		},
		new
		{
			type = "Category Three",
			value = 18
		},
		new
		{
			type = "Category Four",
			value = 15
		},
		new
		{
			type = "Category Five",
			value = 10
		},
		new
		{
			type = "Other",
			value = 5
		}
	};

		readonly PieConfig config2 = new PieConfig
		{
			ForceFit = true,
			Title = new Title
			{
				Visible = true,
				Text = "Pie chart-outer label"
			},
			Description = new Description
			{
				Visible = true,
				Text = "When the type of the pie chart label is set to outer, the label is displayed on the outside of the slice. Set the offset value of the offset control label."
			},
			Radius = 0.8,
			AngleField = "value",
			ColorField = "type",
			Label = new PieLabelConfig
			{
				Visible = true,
				Type = "outer",
				Offset = 20,
			}
		};

		#endregion Example 2

		#region Example 3


		readonly PieConfig config3 = new PieConfig
		{
			ForceFit = true,
			Title = new Title
			{
				Visible = true,
				Text = "Pie Chart-External Circular Graph Label (outer-center label)"
			},
			Description = new Description
			{
				Visible = true,
				Text = "When the type of the pie chart label is set to outer-center, the label is displayed on the outside of the slice. When the label of the outer-center layout is occluded, it will be directly hidden without shifting and avoiding. Compared with the outer label layout, it is more beautiful"
			},
			Radius = 0.8,
			AngleField = "value",
			ColorField = "type",
			Label = new PieLabelConfig
			{
				Visible = true,
				Type = "outer-center",
			}
		};

		#endregion Example 3

		#region Example 4

		readonly object[] data4 =
		{
		new
		{
			type = "Category One",
			value = 27
		},
		new
		{
			type = "Category Two",
			value = 25
		},
		new
		{
			type = "Category Three",
			value = 18
		},
		new
		{
			type = "Category Four",
			value = 15
		},
		new
		{
			type = "Category Five",
			value = 10
		},
		new
		{
			type = "Other",
			value = 5
		}
	};

		readonly PieConfig config4 = new PieConfig
		{
			ForceFit = true,
			Title = new Title
			{
				Visible = true,
				Text = "Pie Chart-Graphic Tab Spider Layout"
			},
			Description = new Description
			{
				Visible = true,
				Text = "When the type of the pie chart label is set to spider, the labels are divided into two groups, and they are displayed in alignment by pulling lines on both sides of the chart. Generally speaking, the labels of the spider layout are less likely to block each other."
			},
			Radius = 0.8,
			AngleField = "value",
			ColorField = "type",
			Label = new PieLabelConfig
			{
				Visible = true,
				Type = "spider"
			}
		};

		#endregion Example 4


		public IChartComponent chart;





		readonly object[] Radardata2 =
   {
		  new {
			item =  "Design",
			user =  "a",
			score =  70,
		  },
		  new {
			item =  "Design",
			user =  "b",
			score =  30,
		  },
		  new {
			item =  "Development",
			user =  "a",
			score =  60,
		  },
		  new {
			item =  "Development",
			user =  "b",
			score =  70,
		  },
		  new {
			item =  "Marketing",
			user =  "a",
			score =  60,
		  },
		  new {
			item =  "Marketing",
			user =  "b",
			score =  50,
		  },
		  new {
			item =  "Users",
			user =  "a",
			score =  40,
		  },
		  new {
			item =  "Users",
			user =  "b",
			score =  50,
		  },
		  new {
			item =  "Test",
			user =  "a",
			score =  60,
		  },
		  new {
			item =  "Test",
			user =  "b",
			score =  70,
		  },
		  new {
			item =  "Language",
			user =  "a",
			score =  70,
		  },
		  new {
			item =  "Language",
			user =  "b",
			score =  50,
		  },
		  new {
			item =  "Technology",
			user =  "a",
			score =  50,
		  },
		  new {
			item =  "Technology",
			user =  "b",
			score =  40,
		  },
		  new {
			item =  "Support",
			user =  "a",
			score =  30,
		  },
		  new {
			item =  "Support",
			user =  "b",
			score =  40,
		  },
		  new {
			item =  "Sales",
			user =  "a",
			score =  60,
		  },
		  new {
			item =  "Sales",
			user =  "b",
			score =  40,
		  },
		  new {
			item =  "UX",
			user =  "a",
			score =  50,
		  },
		  new {
			item =  "UX",
			user =  "b",
			score =  60,
		  },
	};

		readonly RadarConfig Radarconfig2 = new RadarConfig
		{
			Title = new Title
			{
				Visible = true,
				Text = "多组雷达图"
			},

			SeriesField = "user",
			RadiusAxis = new ValueAxis
			{
				Grid = new BaseAxisGrid
				{
					Line = new BaseAxisGridLine
					{
						Type = "line"
					}
				}
			},
			Line = new RadarViewConfigLine
			{
				Visible = true,
			},
			Point = new RadarViewConfigPoint
			{
				Visible = true,
				Shape = "circle"
			},
			Legend = new Legend
			{
				Visible = true,
				Position = "bottom-center"
			},

		};


	}
}