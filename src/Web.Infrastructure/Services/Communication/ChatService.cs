using DCMS.Application.Models.Chat;
using DCMS.Application.Models.Identity;
using DCMS.Web.Infrastructure.Extensions;
using DCMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DCMS.Application.Interfaces.Chat;

namespace DCMS.Web.Infrastructure.Services.Communication
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<IEnumerable<ChatHistoryModel>>> GetChatHistoryAsync(string cId)
        {
            var response = await _httpClient.GetAsync(Routes.ChatEndpoint.GetChatHistory(cId));
            var data = await response.ToResult<IEnumerable<ChatHistoryModel>>();
            return data;
        }

        public async Task<IResult<IEnumerable<ChatUserModel>>> GetChatUsersAsync()
        {
            var response = await _httpClient.GetAsync(Routes.ChatEndpoint.GetAvailableUsers);
            var data = await response.ToResult<IEnumerable<ChatUserModel>>();
            return data;
        }

        public async Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ChatEndpoint.SaveMessage, chatHistory);
            var data = await response.ToResult();
            return data;
        }
    }
}