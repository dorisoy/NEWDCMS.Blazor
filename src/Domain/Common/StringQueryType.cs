namespace DCMS.Domain.Common
{

    public partial class StringQueryType
    {
        /// <summary>
        /// Gets or sets a value
        /// </summary>
        public string Value { get; set; }
    }


    public class UserQueryType
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserRealName { get; set; }
    }
}