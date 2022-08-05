namespace DCMS.Domain.Main
{

	public partial class DiscountManufacturerMapping : AuditableEntity<int>
    {
        public int DiscountId { get; set; }


        public int ManufacturerId { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}