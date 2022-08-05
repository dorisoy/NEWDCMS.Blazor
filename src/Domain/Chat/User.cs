namespace DCMS.Domain.Chat
{
    public class User : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string MobileNumber { get; set; }
        public int UserId { get; set; }
        public string OpenId { get; set; }
    }
}
