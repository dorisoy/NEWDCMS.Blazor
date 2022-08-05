using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.Domain
{
	public abstract class BaseAccountEntity<TId> : IEntity<TId>
	{
		public TId Id { get; set; }

		/// <summary>
		/// 租户标识
		/// </summary>
		public int StoreId { get; set; }

        /// 科目类别
        /// </summary>
        [NotMapped]
        public int AccountingTypeId { get; set; }

        /// <summary>
        /// 财务科目
        /// </summary>
        [NotMapped]
        public string AccountingOptionName { get; set; }

        /// <summary>
        /// 会计科目
        /// </summary>
        public int AccountingOptionId { get; set; }

        /// <summary>
        /// 单据Id
        /// </summary>
        public int BillId { get; set; }

        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal CollectionAmount { get; set; }

    }

}
