using DCMS.Application.Models.Chat;
using DCMS.Application.Models.Identity;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCMS.Application.Interfaces.Chat;
using DCMS.Shared.Services;

namespace DCMS.Web.Infrastructure.Services.Communication
{
    public interface IChatService : IService
    {
        Task<IResult<IEnumerable<ChatUserModel>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryModel>>> GetChatHistoryAsync(string cId);
    }
}