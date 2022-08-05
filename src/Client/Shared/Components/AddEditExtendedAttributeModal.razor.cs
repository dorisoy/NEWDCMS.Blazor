using System;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using DCMS.Application.Features.ExtendedAttributes.Commands.AddEdit;
using DCMS.Client.Extensions;
using DCMS.Web.Infrastructure.Services.ExtendedAttribute;
using DCMS.Domain;
using DCMS.Domain.Enums;
using DCMS.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace DCMS.Client.Shared.Components
{
    public class AddEditExtendedAttributeModalLocalization
    {
        // for localization
    }

    public partial class AddEditExtendedAttributeModal<TId, TEntityId, TEntity, TExtendedAttribute>
        where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
        where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
        where TId : IEquatable<TId>
    {
        [Inject] private IExtendedAttributeService<TId, TEntityId, TEntity, TExtendedAttribute> ExtendedAttributeService { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public AddEditExtendedAttributeCommand<TId, TEntityId, TEntity, TExtendedAttribute> AddEditExtendedAttributeModel { get; set; } = new();

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        private MudDatePicker _datePicker;
        private MudTimePicker _timePicker;
        private TimeSpan? _time;

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            switch (AddEditExtendedAttributeModel.Type)
            {
                case EntityExtendedAttributeType.Decimal:
                    AddEditExtendedAttributeModel.DateTime = null;
                    AddEditExtendedAttributeModel.Text = null;
                    AddEditExtendedAttributeModel.Json = null;
                    break;
                case EntityExtendedAttributeType.Text:
                    AddEditExtendedAttributeModel.Decimal = null;
                    AddEditExtendedAttributeModel.DateTime = null;
                    AddEditExtendedAttributeModel.Json = null;
                    break;
                case EntityExtendedAttributeType.DateTime:
                    AddEditExtendedAttributeModel.DateTime ??= new DateTime(0, 0, 0);
                    AddEditExtendedAttributeModel.DateTime += _time ?? new TimeSpan(0, 0, 0);
                    AddEditExtendedAttributeModel.Decimal = null;
                    AddEditExtendedAttributeModel.Text = null;
                    AddEditExtendedAttributeModel.Json = null;
                    break;
                case EntityExtendedAttributeType.Json:
                    AddEditExtendedAttributeModel.Decimal = null;
                    AddEditExtendedAttributeModel.Text = null;
                    AddEditExtendedAttributeModel.DateTime = null;
                    break;
            }

            var Model = await ExtendedAttributeService.SaveAsync(AddEditExtendedAttributeModel);
            if (Model.Succeeded)
            {
                _snackBar.Add(Model.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in Model.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
            await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_NavigationManager, _localStorage);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}