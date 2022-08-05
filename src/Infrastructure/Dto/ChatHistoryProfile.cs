using AutoMapper;
using DCMS.Application.Interfaces.Chat;
using DCMS.Application.Models.Chat;
using DCMS.Infrastructure.Models.Identity;

namespace DCMS.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<AppUser>>().ReverseMap();
        }
    }
}