using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using DCMS.Shared.Constants.Application;
using Blazored.LocalStorage;

namespace DCMS.Client.Extensions
{
    public static class HubExtensions
    {
        public static HubConnection TryInitialize(this HubConnection hubConnection, NavigationManager NavigationManager, ILocalStorageService _localStorage)
        {
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                                  .WithUrl(NavigationManager.ToAbsoluteUri(ApplicationConstants.SignalR.HubUrl), options => {
                                      options.AccessTokenProvider = async () => (await _localStorage.GetItemAsync<string>("authToken"));
                                  })
                                  .WithAutomaticReconnect()
                                  .Build();
            }
            return hubConnection;
        }
        public static HubConnection TryInitialize(this HubConnection hubConnection, NavigationManager NavigationManager)
        {
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                                  .WithUrl(NavigationManager.ToAbsoluteUri(ApplicationConstants.SignalR.HubUrl))
                                  .Build();
            }
            return hubConnection;
        }
    }
}