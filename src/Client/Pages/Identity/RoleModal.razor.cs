using DCMS.Application.Requests.Identity;
using DCMS.Client.Extensions;
using DCMS.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using DCMS.Web.Infrastructure.Services.Identity.Roles;

namespace DCMS.Client.Pages.Identity
{
    public partial class RoleModal
    {
        [Inject] private IRoleService RoleService { get; set; }

        [Parameter] public RoleRequest RoleModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        protected override async Task OnInitializedAsync()
        {
            HubConnection = HubConnection.TryInitialize(_NavigationManager, _localStorage);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task SaveAsync()
        {
            var Model = await RoleService.SaveAsync(RoleModel);
            if (Model.Succeeded)
            {
                _snackBar.Add(Model.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in Model.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}