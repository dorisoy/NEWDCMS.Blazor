using DCMS.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Shared.Dialogs
{
    public partial class Logout
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public HubConnection HubConnection { get; set; }

        [Parameter] public string ContentText { get; set; }

        [Parameter] public string ButtonText { get; set; }

        [Parameter] public Color Color { get; set; }

        [Parameter] public string CurrentUserId { get; set; }

        async Task Submit()
        {
            await HubConnection.SendAsync(ApplicationConstants.SignalR.OnDisconnect, CurrentUserId);
            await _authenticationService.Logout();
            _NavigationManager.NavigateTo("/login");
            MudDialog.Close(DialogResult.Ok(true));
        }
        void Cancel() => MudDialog.Cancel();
    }
}
