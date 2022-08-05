namespace DCMS.Domain.Main
{
    public class ProductCategory : AuditableEntity<int>
    {
        public int ProductId { get; set; } = 0;
        public int CategoryId { get; set; } = 0;
        public int DisplayOrder { get; set; } = 0;

        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}
