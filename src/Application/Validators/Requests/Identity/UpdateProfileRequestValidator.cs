using DCMS.Application.Requests.Identity;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DCMS.Application.Validators.Requests.Identity
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator(IStringLocalizer<UpdateProfileRequestValidator> localizer)
        {
            RuleFor(request => request.UserRealName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["真实姓名不能为空"]);

        }
    }
}