namespace DCMS.Domain.Common
{
    public partial class SearchTerm : AuditableEntity<int>
    {

        public string Keyword { get; set; }



        public int Count { get; set; }
    }
}
