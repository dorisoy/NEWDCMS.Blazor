using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMS.Shared.Models;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services
{
	public interface IMenuService : IService
	{
		IEnumerable<Menu> Sales { get; }
		IEnumerable<Menu> Purchases { get; }
		IEnumerable<Menu> WareHouses { get; }
		IEnumerable<Menu> Finances { get; }
		IEnumerable<Menu> Archives { get; }
		IEnumerable<Menu> Reports { get; }
	}

	public class MenuService : IMenuService
	{

		private IEnumerable<Menu> _sales;
		private IEnumerable<Menu> _purchases;
		private IEnumerable<Menu> _warehouses;
		private IEnumerable<Menu> _finances;
		private IEnumerable<Menu> _archives;
		private IEnumerable<Menu> _reports;


		public IEnumerable<Menu> Sales => _sales ??= new MainMenus()

			.AddNavGroup("Sales", "销售单据", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售订单")
			.AddItem("ReturnReservationBill", "退货订单")
			.AddItem("SaleBill", "销售单")
			.AddItem("ReturnBill", "退货单")
			.AddItem("CarGoodBill", "车辆对货单")
			.AddItem("FinanceReceiveAccount", "收款对账单")
			.AddItem("PickingBill", "仓库分拣")
			.AddItem("DispatchBill", "装车调度")
			.AddItem("ChangeReservation", "订单转销售单")
			.AddItem("ExchangeBill", "换货单"))

			.AddNavGroup("SaleReports", "销售报表", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售明细表")
			.AddItem("ReturnReservationBill", "销售汇总(按商品)")
			.AddItem("SaleBill", "销售汇总(按客户)")
			.AddItem("ReturnBill", "销售汇总(按业务员)")
			.AddItem("CarGoodBill", "销售汇总(客户/商品)")
			.AddItem("FinanceReceiveAccount", "销售汇总(按仓库)")
			.AddItem("PickingBill", "销售汇总(按品牌)")
			.AddItem("DispatchBill", "装车调度")
			.AddItem("ChangeReservation", "订单明细")
			.AddItem("ExchangeBill", "订单汇总(按商品)")
			.AddItem("ChangeReservation", "订单明细")
			.AddItem("ChangeReservation", "费用合同明细表")
			.AddItem("ChangeReservation", "订单汇总(按商品)")
			.AddItem("ChangeReservation", "费用合同明细表")
			.AddItem("ChangeReservation", "赠品汇总")
			.AddItem("ChangeReservation", "热销排行榜")
			.AddItem("ChangeReservation", "销量走势图")
			.AddItem("ChangeReservation", "销售商品成本利润")
			.AddItem("ChangeReservation", "销售额分析")
			.AddItem("ChangeReservation", "订单额分析")
			.AddItem("ChangeReservation", "热订排行榜")
			.AddItem("ChangeReservation", "经营日报")
			.AddItem("ChangeReservation", "经营月报")
			.AddItem("ChangeReservation", "经营年报"))

			.GetMenusSortedByName();

		
		public IEnumerable<Menu> Purchases => _purchases ??= new MainMenus()

			.AddNavGroup("Sales", "销售单据", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售订单")
			.AddItem("ReturnReservationBill", "退货订单")
			.AddItem("SaleBill", "销售单")
			.AddItem("ReturnBill", "退货单")
			.AddItem("CarGoodBill", "车辆对货单")
			.AddItem("FinanceReceiveAccount", "收款对账单")
			.AddItem("PickingBill", "仓库分拣")
			.AddItem("DispatchBill", "装车调度")
			.AddItem("ChangeReservation", "订单转销售单")
			.AddItem("ExchangeBill", "换货单"))

			.AddNavGroup("SaleReports", "销售报表", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售明细表")
			.AddItem("ChangeReservation", "经营日报")
			.AddItem("ChangeReservation", "经营月报")
			.AddItem("ChangeReservation", "经营年报"))

			.GetMenusSortedByName();


		public IEnumerable<Menu> WareHouses => _warehouses ??= new MainMenus()

			.AddNavGroup("Sales", "销售单据", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售订单")
			.AddItem("ReturnReservationBill", "退货订单")
			.AddItem("SaleBill", "销售单")
			.AddItem("ReturnBill", "退货单")
			.AddItem("CarGoodBill", "车辆对货单")
			.AddItem("FinanceReceiveAccount", "收款对账单")
			.AddItem("PickingBill", "仓库分拣")
			.AddItem("DispatchBill", "装车调度")
			.AddItem("ChangeReservation", "订单转销售单")
			.AddItem("ExchangeBill", "换货单"))

			.AddNavGroup("SaleReports", "销售报表", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售明细表")
			.AddItem("ChangeReservation", "经营日报")
			.AddItem("ChangeReservation", "经营月报")
			.AddItem("ChangeReservation", "经营年报"))

			.GetMenusSortedByName();


		public IEnumerable<Menu> Finances => _finances ??= new MainMenus()

			.AddNavGroup("Sales", "销售单据", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售订单")
			.AddItem("ReturnReservationBill", "退货订单")
			.AddItem("SaleBill", "销售单")
			.AddItem("ReturnBill", "退货单")
			.AddItem("CarGoodBill", "车辆对货单")
			.AddItem("FinanceReceiveAccount", "收款对账单")
			.AddItem("PickingBill", "仓库分拣")
			.AddItem("DispatchBill", "装车调度")
			.AddItem("ChangeReservation", "订单转销售单")
			.AddItem("ExchangeBill", "换货单"))

			.AddNavGroup("SaleReports", "销售报表", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售明细表")
			.AddItem("ChangeReservation", "经营日报")
			.AddItem("ChangeReservation", "经营月报")
			.AddItem("ChangeReservation", "经营年报"))

			.GetMenusSortedByName();


		public IEnumerable<Menu> Archives => _archives ??= new MainMenus()

			.AddNavGroup("Sales", "销售单据", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售订单")
			.AddItem("ReturnReservationBill", "退货订单")
			.AddItem("SaleBill", "销售单")
			.AddItem("ReturnBill", "退货单")
			.AddItem("CarGoodBill", "车辆对货单")
			.AddItem("FinanceReceiveAccount", "收款对账单")
			.AddItem("PickingBill", "仓库分拣")
			.AddItem("DispatchBill", "装车调度")
			.AddItem("ChangeReservation", "订单转销售单")
			.AddItem("ExchangeBill", "换货单"))

			.AddNavGroup("SaleReports", "销售报表", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售明细表")
			.AddItem("ChangeReservation", "经营日报")
			.AddItem("ChangeReservation", "经营月报")
			.AddItem("ChangeReservation", "经营年报"))

			.GetMenusSortedByName();

		public IEnumerable<Menu> Reports => _reports ??= new MainMenus()

			.AddNavGroup("Sales", "销售单据", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售订单")
			.AddItem("ReturnReservationBill", "退货订单")
			.AddItem("SaleBill", "销售单")
			.AddItem("ReturnBill", "退货单")
			.AddItem("CarGoodBill", "车辆对货单")
			.AddItem("FinanceReceiveAccount", "收款对账单")
			.AddItem("PickingBill", "仓库分拣")
			.AddItem("DispatchBill", "装车调度")
			.AddItem("ChangeReservation", "订单转销售单")
			.AddItem("ExchangeBill", "换货单"))

			.AddNavGroup("SaleReports", "销售报表", false, new MainMenus()
			.AddItem("SaleReservationBill", "销售明细表")
			.AddItem("ChangeReservation", "经营日报")
			.AddItem("ChangeReservation", "经营月报")
			.AddItem("ChangeReservation", "经营年报"))

			.GetMenusSortedByName();

	}
}




