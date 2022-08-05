namespace DCMS.Domain.Common
{

    /// <summary>
    /// 表示泛型属性
    /// </summary>
    public partial class GenericAttribute : AuditableEntity<int>
    {

        public int EntityId { get; set; }


        public string KeyGroup { get; set; }


        public string Key { get; set; }


        public string Value { get; set; }



    }
}
