using System.Threading.Tasks;
using DCMS.Application.Models;
using DCMS.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using AntDesign;
using Blazored.FluentValidation;
using DCMS.Application.Requests.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;


namespace DCMS.Web.Pages.Account.Login
{
	public partial class Login
	{

		private FluentValidationValidator _fluentValidationValidator;
		private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

		private TokenRequest _tokenModel = new();


        protected override async Task OnInitializedAsync()
		{
			FillAdministratorCredentials();

			var state = await _stateProvider.GetAuthenticationStateAsync();
			if (state?.User?.Identity?.IsAuthenticated == true)
			{
				_navigationManager.NavigateTo("/");
			}
		}

		private async Task SubmitAsync()
		{
			var result = await _authenticationService.Login(_tokenModel);
			if (!result.Succeeded)
			{
				foreach (var message in result.Messages)
				{
					await _message.Warning(message);
				}
			}

			_navigationManager.NavigateTo("/");
		}

		private bool _passwordVisibility;
	   // private InputType _passwordInput = InputType.Password;
	   // private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

		void TogglePasswordVisibility()
		{
			if (_passwordVisibility)
			{
				_passwordVisibility = false;
				//_passwordInputIcon = Icons.Material.Filled.VisibilityOff;
				//_passwordInput = InputType.Password;
			}
			else
			{
				_passwordVisibility = true;
				//_passwordInputIcon = Icons.Material.Filled.Visibility;
				//_passwordInput = InputType.Text;
			}
		}

		private void FillAdministratorCredentials()
		{
			_tokenModel.Email = "admin@jsdcms.com";
			_tokenModel.Password = "123Pa$$word!";
		}
	}
}