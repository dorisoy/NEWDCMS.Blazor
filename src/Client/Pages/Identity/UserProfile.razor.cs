using DCMS.Application.Requests.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Identity
{
    public partial class UserProfile
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }

        private bool _active;
        private char _firstLetterOfName;
        private string _UserRealName;
        private string _phoneNumber;
        private string _email;

        private bool _loaded;

        private async Task ToggleUserStatus()
        {
            var request = new ToggleUserStatusRequest { ActivateUser = _active, UserId = Id };
            var result = await _userService.ToggleUserStatusAsync(request);
            if (result.Succeeded)
            {
                _snackBar.Add(_localizer["Updated User Status."], Severity.Success);
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

        [Parameter] public string ImageDataUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var userId = Id;
            var result = await _userService.GetAsync(userId);
            if (result.Succeeded)
            {
                var user = result.Data;
                if (user != null)
                {
                    _UserRealName = user.UserRealName;
                    _email = user.Email;
                    _phoneNumber = user.PhoneNumber;
                    _active = user.IsActive;
                    var data = await _accountService.GetProfilePictureAsync(userId);
                    if (data.Succeeded)
                    {
                        ImageDataUrl = data.Data;
                    }
                }
                Title = $"{_UserRealName} 's {_localizer["Profile"]}";
                Description = _email;
                if (_UserRealName.Length > 0)
                {
                    _firstLetterOfName = _UserRealName[0];
                }
            }

            _loaded = true;
        }
    }
}