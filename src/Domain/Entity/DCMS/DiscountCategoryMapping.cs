namespace DCMS.Domain.Main
{

	public partial class DiscountCategoryMapping : AuditableEntity<int>
    {
        public int DiscountId { get; set; }
        public int CategoryId { get; set; }
        public virtual Discount Discount { get; set; }

        public virtual Category Category { get; set; }
    }
}