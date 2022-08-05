namespace DCMS.Domain.Main
{

	/// <summary>
	/// 用于表示商品图片
	/// </summary>
	public partial class ProductPicture : AuditableEntity<int>
    {

        public int ProductId { get; set; } = 0;

        public int PictureId { get; set; } = 0;


        public int DisplayOrder { get; set; } = 0;

        
        public virtual Picture Picture { get; set; }
        
        public virtual Product Product { get; set; }
    }

}
