using FluentValidation;
using TaskManagerWebApi.Models.Enums;
using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Validators
{
    public class ProjectRequestBaseValidator<T> : AbstractValidator<T> where T : ProjectRequestBase
    {
        protected ProjectRequestBaseValidator()
        {
            int titleLen = 200;
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(titleLen).WithMessage($"Title may not exceed {titleLen} characters");

            int descrLen = 5000;
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(descrLen).WithMessage($"Description may not exceed {descrLen} characters");

            RuleFor(x => x.Start)
                .NotEmpty().WithMessage("Start date is required")
                .GreaterThan(DateTime.Now).WithMessage("Deadline cannot be in the past")
                .LessThan(x => x.Deadline).WithMessage("Start time cannot be less than deadline");

            int maxYears = 10;
            RuleFor(x => x.Deadline)
                .NotEmpty().WithMessage("Deadline is required")
                .GreaterThan(DateTime.Now).WithMessage("Deadline cannot be in the past")
                .LessThan(DateTime.Now.AddYears(maxYears)).WithMessage($"Deadline may not exceed {maxYears} years");
        }
    }
}
