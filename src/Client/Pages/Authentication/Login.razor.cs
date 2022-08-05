using Blazored.FluentValidation;
using DCMS.Application.Requests.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Authentication
{
    public partial class Login
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated 
        {
            get 
            {
                return _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
            }
        }

		private TokenRequest _tokenModel = new()
		{
			Email = "admin@jsdcms.com",
			Password = "123Pa$$word!"
		};

		protected override async Task OnInitializedAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            if (state?.User?.Identity?.IsAuthenticated == true)
            {
                _NavigationManager.NavigateTo("/home");
            }
        }

        private async Task SubmitAsync()
        {
            var result = await _authenticationService.Login(_tokenModel);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }

        private void FillAdministratorCredentials()
        {
            _tokenModel.Email = "admin@jsdcms.com";
            _tokenModel.Password = "123Pa$$word!";
        }

        private void FillBasicUserCredentials()
        {
            _tokenModel.Email = "john@jsdcms.com";
            _tokenModel.Password = "123Pa$$word!";
        }
    }
}