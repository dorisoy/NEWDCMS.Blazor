namespace DCMS.Application.Models.Identity
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserRealName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureDataUrl { get; set; }
    }
}