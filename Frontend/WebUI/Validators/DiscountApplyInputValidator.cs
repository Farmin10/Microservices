using FluentValidation;
using WebUI.Models.DiscountVM;

namespace WebUI.Validators
{
    public class DiscountApplyInputValidator:AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("This area cannot be null");
        }
    }
}
