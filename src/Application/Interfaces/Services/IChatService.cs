using DCMS.Application.Interfaces.Chat;
using DCMS.Application.Models.Chat;
using DCMS.Application.Models.Identity;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserModel>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryModel>>> GetChatHistoryAsync(string userId, string contactId);
    }
}