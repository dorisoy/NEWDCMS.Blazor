namespace DCMS.Application.Interfaces.Chat
{
    public interface IChatUser
    {
        public string UserName { get; set; }
        public string UserRealName { get; set; }

        public string ProfilePictureDataUrl { get; set; }
    }
}