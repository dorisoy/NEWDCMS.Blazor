using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DCMS.Application.Features.ExtendedAttributes.Commands.AddEdit;
using DCMS.Application.Features.ExtendedAttributes.Queries.Export;
using DCMS.Application.Features.ExtendedAttributes.Queries.GetAllByEntityId;
using DCMS.Client.Extensions;
using DCMS.Web.Infrastructure.Services.ExtendedAttribute;
using DCMS.Domain;
using DCMS.Domain.Enums;
using DCMS.Shared.Constants.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;

namespace DCMS.Client.Shared.Components
{
    public class ExtendedAttributesLocalization
    {
        // for localization
    }

    public abstract partial class ExtendedAttributesBase<TId, TEntityId, TEntity, TExtendedAttribute>
        where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
        where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
        where TId : IEquatable<TId>
    {
        [Inject] private IExtendedAttributeService<TId, TEntityId, TEntity, TExtendedAttribute> ExtendedAttributeService { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [Parameter] public string EntityIdString { get; set; }
        [Parameter] public string EntityName { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }

        protected abstract Func<string, TEntityId> FromStringToEntityIdTypeConverter { get; }
        protected abstract string ExtendedAttributesViewPolicyName { get; }
        protected abstract string ExtendedAttributesEditPolicyName { get; }
        protected abstract string ExtendedAttributesCreatePolicyName { get; }
        protected abstract string ExtendedAttributesDeletePolicyName { get; }
        protected abstract string ExtendedAttributesExportPolicyName { get; }
        protected abstract string ExtendedAttributesSearchPolicyName { get; }
        protected abstract RenderFragment Inherited();

        private TEntityId EntityId => FromStringToEntityIdTypeConverter.Invoke(EntityIdString);
        private string CurrentUserId { get; set; }
        private List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>> _model;
        private Dictionary<string, List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>>> GroupedExtendedAttributes { get; } = new();
        private GetAllExtendedAttributesByEntityIdModel<TId, TEntityId> _extendedAttributes = new();
        private GetAllExtendedAttributesByEntityIdModel<TId, TEntityId> _selectedItem = new();
        private string _searchString = "";
        private bool _includeEntity;
        private bool _onlyCurrentGroup;
        private int _activeGroupIndex;
        private MudTabs _mudTabs;
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canViewExtendedAttributes;
        private bool _canEditExtendedAttributes;
        private bool _canCreateExtendedAttributes;
        private bool _canDeleteExtendedAttributes;
        private bool _canExportExtendedAttributes;
        private bool _canSearchExtendedAttributes;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationService.CurrentUser();
            _canViewExtendedAttributes = (await _authorizationService.AuthorizeAsync(_currentUser, ExtendedAttributesViewPolicyName)).Succeeded;
            if (!_canViewExtendedAttributes)
            {
                _snackBar.Add(_localizer["Not Allowed."], Severity.Error);
                _NavigationManager.NavigateTo("/");
            }
            _canEditExtendedAttributes = (await _authorizationService.AuthorizeAsync(_currentUser, ExtendedAttributesEditPolicyName)).Succeeded;
            _canCreateExtendedAttributes = (await _authorizationService.AuthorizeAsync(_currentUser, ExtendedAttributesCreatePolicyName)).Succeeded;
            _canDeleteExtendedAttributes = (await _authorizationService.AuthorizeAsync(_currentUser, ExtendedAttributesDeletePolicyName)).Succeeded;
            _canExportExtendedAttributes = (await _authorizationService.AuthorizeAsync(_currentUser, ExtendedAttributesExportPolicyName)).Succeeded;
            _canSearchExtendedAttributes = (await _authorizationService.AuthorizeAsync(_currentUser, ExtendedAttributesSearchPolicyName)).Succeeded;

            await GetExtendedAttributesAsync();
            _loaded = true;

            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user == null) return;
            if (user.Identity?.IsAuthenticated == true)
            {
                CurrentUserId = user.GetUserId();
            }

            HubConnection = HubConnection.TryInitialize(_NavigationManager, _localStorage);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetExtendedAttributesAsync()
        {
            var Model = await ExtendedAttributeService.GetAllByEntityIdAsync(EntityId);
            if (Model.Succeeded)
            {
                GroupedExtendedAttributes.Clear();
                _model = Model.Data;
                GroupedExtendedAttributes.Add(_localizer["All Groups"], _model);
                foreach (var extendedAttribute in _model)
                {
                    if (!string.IsNullOrWhiteSpace(extendedAttribute.Group))
                    {
                        if (GroupedExtendedAttributes.ContainsKey(extendedAttribute.Group))
                        {
                            GroupedExtendedAttributes[extendedAttribute.Group].Add(extendedAttribute);
                        }
                        else
                        {
                            GroupedExtendedAttributes.Add(extendedAttribute.Group, new List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>> { extendedAttribute });
                        }
                    }
                }

                if (_model != null)
                {
                    Description = string.Format(_localizer["Manage {0} {1}'s Extended Attributes"], EntityId, EntityName);
                }
            }
            else
            {
                foreach (var message in Model.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
                _NavigationManager.NavigateTo("/");
            }
        }

        private async Task ExportToExcel()
        {
            var request = new ExportExtendedAttributesQuery<TId, TEntityId, TEntity, TExtendedAttribute>
            {
                SearchString = _searchString,
                EntityId = EntityId,
                IncludeEntity = _includeEntity,
                OnlyCurrentGroup = _onlyCurrentGroup && _activeGroupIndex != 0,
                CurrentGroup = _mudTabs.Panels[_activeGroupIndex].Text
            };
            var Model = await ExtendedAttributeService.ExportToExcelAsync(request);
            if (Model.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = Model.Data,
                    FileName = $"{typeof(TExtendedAttribute).Name.ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(request.SearchString) && !request.IncludeEntity && !request.OnlyCurrentGroup
                    ? _localizer["Extended Attributes exported"]
                    : _localizer["Filtered Extended Attributes exported"], Severity.Success);
            }
            else
            {
                foreach (var message in Model.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(TId id = default)
        {
            var parameters = new DialogParameters();
            if (!id.Equals(default))
            {
                var documentExtendedAttribute = _model.FirstOrDefault(c => c.Id.Equals(id));
                if (documentExtendedAttribute != null)
                {
                    parameters.Add(nameof(AddEditExtendedAttributeModal<TId, TEntityId, TEntity, TExtendedAttribute>.AddEditExtendedAttributeModel), new AddEditExtendedAttributeCommand<TId, TEntityId, TEntity, TExtendedAttribute>
                    {
                        Id = documentExtendedAttribute.Id,
                        EntityId = documentExtendedAttribute.EntityId,
                        Type = documentExtendedAttribute.Type,
                        Key = documentExtendedAttribute.Key,
                        Text = documentExtendedAttribute.Text,
                        Decimal = documentExtendedAttribute.Decimal,
                        DateTime = documentExtendedAttribute.DateTime,
                        Json = documentExtendedAttribute.Json,
                        ExternalId = documentExtendedAttribute.ExternalId,
                        Group = documentExtendedAttribute.Group,
                        Description = documentExtendedAttribute.Description,
                        IsActive = documentExtendedAttribute.IsActive
                    });
                }
            }
            else
            {
                parameters.Add(nameof(AddEditExtendedAttributeModal<TId, TEntityId, TEntity, TExtendedAttribute>.AddEditExtendedAttributeModel), new AddEditExtendedAttributeCommand<TId, TEntityId, TEntity, TExtendedAttribute>
                {
                    EntityId = EntityId,
                    Type = EntityExtendedAttributeType.Text
                });
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditExtendedAttributeModal<TId, TEntityId, TEntity, TExtendedAttribute>>(id.Equals(default) ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Delete(TId id)
        {
            string deleteContent = _localizer["Delete Extended Attribute?"];
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var Model = await ExtendedAttributeService.DeleteAsync(id);
                if (Model.Succeeded)
                {
                     await Reset();
                    _snackBar.Add(Model.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in Model.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private async Task Reset()
        {
            _model = new List<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>>();
            _searchString = "";
            await GetExtendedAttributesAsync();
        }

        private Func<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>, object> SortById = Model => Model.Id;
        private Func<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>, object> SortByType = Model => Model.Type;
        private Func<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>, object> SortByKey = Model => Model.Key;
        private Func<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>, object> SortByValue = Model => Model.Type switch
        {
            EntityExtendedAttributeType.Decimal => Model.Decimal,
            EntityExtendedAttributeType.Text => Model.Text,
            EntityExtendedAttributeType.DateTime => Model.DateTime,
            EntityExtendedAttributeType.Json => Model.Json,
            _ => Model.Text
        };
        private Func<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>, object> SortByExternalId = Model => Model.ExternalId;
        private Func<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>, object> SortByGroup = Model => Model.Group;
        private Func<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>, object> SortByDescription = Model => Model.Description;
        private Func<GetAllExtendedAttributesByEntityIdModel<TId, TEntityId>, object> SortByIsActive = Model => Model.IsActive;

        private bool Search(GetAllExtendedAttributesByEntityIdModel<TId, TEntityId> extendedAttributes)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (extendedAttributes.Key.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (extendedAttributes.Text?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (extendedAttributes.Decimal?.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (extendedAttributes.DateTime?.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (extendedAttributes.Json?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (extendedAttributes.ExternalId?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (extendedAttributes.Group?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (extendedAttributes.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private Color GetGroupBadgeColor(int selected, int all)
        {
            if (selected == 0)
                return Color.Error;

            if (selected == all)
                return Color.Success;

            return Color.Info;
        }
    }
}