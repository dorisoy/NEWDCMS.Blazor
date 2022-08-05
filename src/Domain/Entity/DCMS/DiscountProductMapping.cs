namespace DCMS.Domain.Main
{

	public partial class DiscountProductMapping : AuditableEntity<int>
	{

		public int DiscountId { get; set; }

		public int ProductId { get; set; }

		public virtual Discount Discount { get; set; }

		public virtual Product Product { get; set; }
	}
}