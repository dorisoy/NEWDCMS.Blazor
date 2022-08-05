using System;
using System.Collections.Generic;
using System.Text;

namespace DCMS.Domain.Main
{
    public class ProductSkuChangeHistory: AuditableEntity<int>
    {
        /// <summary>
        /// 修改sku的商品ID，商品表主键
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 修改前Sku
        /// </summary>
        public string ChangeBefore { get; set; }
        /// <summary>
        /// 修改后Sku
        /// </summary>
        public string ChangeAfter { get; set; }
        /// <summary>
        /// 变更的单据编号
        /// </summary>
        public int BillId { get; set; }
        /// <summary>
        /// 修改Sku的单据号
        /// </summary>
        public string BillNumber { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public int OpretionUser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ChangeTime { get; set; }
    }
}
