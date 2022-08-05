using DCMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;
using DCMS.Application.Models;

namespace DCMS.Server.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			var currUser = httpContextAccessor.HttpContext?.User;
			var data = currUser?.FindFirstValue(ClaimTypes.UserData);

			if (!string.IsNullOrEmpty(data))
				CurrStore = JsonConvert.DeserializeObject<StoreInfo>(data);

			StoreId = CurrStore?.Id ?? 0;
			UserId = currUser?.FindFirstValue(ClaimTypes.NameIdentifier);
			Claims = currUser?.Claims
				.AsEnumerable()
				.Select(item => new KeyValuePair<string, string>(item.Type, item.Value))
				.ToList();
		}

		public string UserId { get; }
		public int StoreId { get; }
		public StoreInfo CurrStore { get; }
		public List<KeyValuePair<string, string>> Claims { get; set; }
	}
}