using DCMS.Application.Models.Identity;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DCMS.Shared.Constants.Application;
using DCMS.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;

namespace DCMS.Client.Pages.Identity
{
    public partial class Users
    {
        private List<UserModel> _userList = new();
        private UserModel _user = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateUsers;
        private bool _canSearchUsers;
        private bool _canExportUsers;
        private bool _canViewRoles;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationService.CurrentUser();
            _canCreateUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Create)).Succeeded;
            _canSearchUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Search)).Succeeded;
            _canExportUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Export)).Succeeded;
            _canViewRoles = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.View)).Succeeded;

            await GetUsersAsync();
            _loaded = true;
        }

        private async Task GetUsersAsync()
        {
            var Model = await _userService.GetAllAsync();
            if (Model.Succeeded)
            {
                _userList = Model.Data.ToList();
            }
            else
            {
                foreach (var message in Model.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (user.UserRealName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
   
            if (user.Email?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.PhoneNumber?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.UserName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task ExportToExcel()
        {
            var base64 = await _userService.ExportToExcelAsync(_searchString);
            await _jsRuntime.InvokeVoidAsync("Download", new
            {
                ByteArray = base64,
                FileName = $"{nameof(Users).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                MimeType = ApplicationConstants.MimeTypes.OpenXml
            });
            _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                ? _localizer["Users exported"]
                : _localizer["Filtered Users exported"], Severity.Success);
        }

        private  Task InvokeModal()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            //var dialog = _dialogService.Show<RegisterUserModal>(_localizer["Register New User"], parameters, options);
            //var result = await dialog.Result;
            //if (!result.Cancelled)
            //{
            //    await GetUsersAsync();
            //}
            return Task.CompletedTask;
        }

        private void ViewProfile(string userId)
        {
            _NavigationManager.NavigateTo($"/user-profile/{userId}");
        }

        private void Serviceoles(string userId, string email)
        {
            if (email == "admin@DCMS.com") _snackBar.Add(_localizer["Not Allowed."], Severity.Error);
            else _NavigationManager.NavigateTo($"/identity/user-roles/{userId}");
        }
    }
}