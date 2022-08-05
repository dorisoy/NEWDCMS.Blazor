namespace DCMS.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 表示备注设置
    /// </summary>
    public partial class RemarkConfig : AuditableEntity<int>
    {

        /// <summary>
        /// 备注名称       
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参与价格记忆
        /// </summary>
        [Column(TypeName = "BIT(1)")]
        public bool RemberPrice { get; set; }

    }
}
