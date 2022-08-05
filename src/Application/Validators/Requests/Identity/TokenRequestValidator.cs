using DCMS.Application.Requests.Identity;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DCMS.Application.Validators.Requests.Identity
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator(IStringLocalizer<TokenRequestValidator> localizer)
        {
            RuleFor(request => request.Email)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "邮箱不能为空!")
                .EmailAddress().WithMessage(x => "邮箱格式不正确!");

            RuleFor(request => request.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "密码不能为空!");
        }
    }
}
