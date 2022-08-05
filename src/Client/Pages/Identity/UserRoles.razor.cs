using System;
using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DCMS.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;

namespace DCMS.Client.Pages.Identity
{
    public partial class UserRoles
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }
        public List<UserRoleModel> UserRolesList { get; set; } = new();

        private UserRoleModel _userRole = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canEditUsers;
        private bool _canSearchRoles;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationService.CurrentUser();
            _canEditUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Edit)).Succeeded;
            _canSearchRoles = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.Search)).Succeeded;

            var userId = Id;
            var result = await _userService.GetAsync(userId);
            if (result.Succeeded)
            {
                var user = result.Data;
                if (user != null)
                {
                    Title = $"{user.UserRealName}";
                    Description = string.Format(_localizer["Manage {0}'s Roles"], user.UserRealName);
                    var Model = await _userService.GetRolesAsync(user.Id);
                    UserRolesList = Model.Data.UserRoles;
                }
            }

            _loaded = true;
        }

        private async Task SaveAsync()
        {
            _snackBar.Add("Function is disabled in demo mode!", Severity.Warning);
            return;

			var request = new UpdateUserRolesRequest()
            {
                UserId = Id,
                UserRoles = UserRolesList
            };

            var result = await _userService.UpdateRolesAsync(request);
            if (result.Succeeded)
            {
                _snackBar.Add(result.Messages[0], Severity.Success);
                _NavigationManager.NavigateTo("/identity/users");
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
        }

        private bool Search(UserRoleModel userRole)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (userRole.RoleName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (userRole.RoleDescription?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}