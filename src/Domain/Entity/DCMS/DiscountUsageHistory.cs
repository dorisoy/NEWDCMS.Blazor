using System;

namespace DCMS.Domain.Main
{

    public partial class DiscountUsageHistory : AuditableEntity<int>
    {
        public int DiscountId { get; set; }
        public int BillId { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual Discount Discount { get; set; }
    }
}
