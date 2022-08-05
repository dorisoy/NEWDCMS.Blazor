using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.Domain.Main
{
	/*
	/// <summary>
	/// 终端信息
	/// </summary>
	public class Terminal : AuditableEntity<int>
	{
		/// <summary>
		/// 终端名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 助记名
		/// </summary>
		public string MnemonicName { get; set; }
		/// <summary>
		/// 老板姓名
		/// </summary>
		public string BossName { get; set; }
		/// <summary>
		/// 老板电话
		/// </summary>
		public string BossCall { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool Status { get; set; }
		/// <summary>
		/// 最大欠款额度
		/// </summary>
		public decimal? MaxAmountOwed { get; set; }
		/// <summary>
		/// 终端编码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 地址
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }
		/// <summary>
		/// 片区Id
		/// </summary>
		public int DistrictId { get; set; }
		/// <summary>
		/// 渠道Id
		/// </summary>
		public int ChannelId { get; set; }
		/// <summary>
		/// 线路Id
		/// </summary>
		public int? LineId { get; set; } = 0;
		/// <summary>
		/// 客户等级
		/// </summary>
		public int RankId { get; set; }
		/// <summary>
		/// 付款方式
		/// </summary>
		public int PaymentMethod { get; set; }
		/// <summary>
		/// 经度
		/// </summary>
		public double? Location_Lng { get; set; }
		/// <summary>
		/// 纬度
		/// </summary>
		public double? Location_Lat { get; set; }

		/// <summary>
		/// 营业编号
		/// </summary>
		public string BusinessNo { get; set; }
		/// <summary>
		/// 食品经营许可证号
		/// </summary>
		public string FoodBusinessLicenseNo { get; set; }
		/// <summary>
		/// 企业注册号
		/// </summary>
		public string EnterpriseRegNo { get; set; }
		/// <summary>
		/// 是否删除
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool Deleted { get; set; }

		/// <summary>
		/// 创建人
		/// </summary>
		public int? CreatedUserId { get; set; } = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedOnUtc { get; set; }

		/// <summary>
		/// 数据类型 注意：这里 包含终端，供应商，选择终端弹出时使用。
		/// Mapping 时忽略此字段，Ignore(c => c.DataTypeId);
		/// </summary>
		public int DataTypeId { get; set; } = 0;

		/// <summary>
		/// 门头照片
		/// </summary>
		public string DoorwayPhoto { get; set; }

		/// <summary>
		/// 是否关联业务
		/// </summary>
		[Column(TypeName = "BIT(1)")]
		public bool Related { get; set; }


		#region 协议信息

		/// <summary>
		/// 是否协议店
		/// </summary>
		public bool IsAgreement { get; set; }

		#endregion


		#region 合作信息

		/// <summary>
		/// 合作意向
		/// </summary>
		public string Cooperation { get; set; }
		/// <summary>
		/// 展示是否陈列
		/// </summary>
		public bool IsDisplay { get; set; }
		/// <summary>
		/// 展示是否生动化
		/// </summary>
		public bool IsVivid { get; set; }
		/// <summary>
		/// 展示是否促销
		/// </summary>
		public bool IsPromotion { get; set; }
		/// <summary>
		/// 展示其它
		/// </summary>
		public string OtherRamark { get; set; }


		#endregion
	}
	*/



	/// <summary>
	/// 用于表示经销商账户余额
	/// </summary>
	public class TerminalBalance : AuditableEntity<int>
	{
		/// <summary>
		/// 科目Id
		/// </summary>
		public int AccountingOptionId { get; set; }

		/// <summary>
		/// 科目名称
		/// </summary>
		public string AccountingName { get; set; }

		/// <summary>
		/// 最大欠款额度
		/// </summary>
		public decimal MaxOweCashBalance { get; set; } = 0;
		/// <summary>
		/// 剩余预收款金额
		/// </summary>
		public decimal AdvanceAmountBalance { get; set; } = 0;
		/// <summary>
		/// 总欠款
		/// </summary>
		public decimal TotalOweCash { get; set; } = 0;
		/// <summary>
		/// 剩余欠款额度
		/// </summary>
		public decimal OweCashBalance { get; set; } = 0;

	}

	public class NewTerminal : AuditableEntity<int>
	{
		public int CreatedUserId { get; set; }
		public int TerminalId { get; set; }
		public int Status { get; set; }
		public DateTime CreatedOnUtc { get; set; }
	}

}
