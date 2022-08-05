using DCMS.Client.Extensions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DCMS.Client.Shared.Components
{
    public partial class UserCard
    {
        [Parameter] public string Class { get; set; }
        private string UserRealName { get; set; }
        private string Email { get; set; }
        private char FirstLetterOfName { get; set; }

        [Parameter]
        public string ImageDataUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;

            this.Email = user.GetEmail().Replace(".com", string.Empty);
            this.UserRealName = user.GetUserRealName();
            if (this.UserRealName.Length > 0)
            {
                FirstLetterOfName = UserRealName[0];
            }
            var UserId = user.GetUserId();
            var imageModel = await _accountService.GetProfilePictureAsync(UserId);
            if (imageModel.Succeeded)
            {
                ImageDataUrl = imageModel.Data;
            }
        }
    }
}