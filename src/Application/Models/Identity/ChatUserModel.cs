using DCMS.Application.Interfaces.Chat;
using DCMS.Application.Models.Chat;
using System.Collections.Generic;

namespace DCMS.Application.Models.Identity
{
    public class ChatUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ProfilePictureDataUrl { get; set; }
        public string UserRealName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsOnline { get; set; }
        public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryFromUsers { get; set; }
        public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryToUsers { get; set; }
    }
}