using DCMS.Application.Interfaces.Services;
using System;

namespace DCMS.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}