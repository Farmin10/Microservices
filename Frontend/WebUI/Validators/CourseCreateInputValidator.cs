using FluentValidation;
using WebUI.Models.CatalogVM;

namespace WebUI.Validators
{
    public class CourseCreateInputValidator:AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name area cannot be null!!!");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category name must be selected!!!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description area cannot be null!!!");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Duration cannot be null!!!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price area cannot be null!!!").ScalePrecision(2, 6).WithMessage("Incorrect format");
        }
    }
}
