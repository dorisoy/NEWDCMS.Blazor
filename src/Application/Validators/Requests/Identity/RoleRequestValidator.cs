using DCMS.Application.Requests.Identity;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DCMS.Application.Validators.Requests.Identity
{
    public class RoleRequestValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequestValidator(IStringLocalizer<RoleRequestValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["名称不能为空"]);
        }
    }
}
