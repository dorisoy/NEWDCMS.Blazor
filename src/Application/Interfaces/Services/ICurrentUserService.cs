using DCMS.Application.Interfaces.Common;
using DCMS.Application.Models;


namespace DCMS.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
        int StoreId { get; }
        StoreInfo CurrStore { get; }
    }
}