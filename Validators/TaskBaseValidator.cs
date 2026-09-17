using FluentValidation;
using TaskManagerWebApi.Models.Enums;
using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Validators
{
    public abstract class TaskBaseValidator<T> : AbstractValidator<T> where T : TaskBaseRequest
    {
        protected TaskBaseValidator()
        {
            int titleLen = 200;
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(titleLen).WithMessage($"Title may not exceed {titleLen} characters");

            int descrLen = 5000;
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(descrLen).WithMessage($"Description may not exceed {descrLen} characters");

            int maxHours = 100;
            RuleFor(x => x.HoursToDo)
                .GreaterThan(0).WithMessage("Number of hours must be greater than 0")
                .LessThanOrEqualTo(maxHours).WithMessage($"Number of hours may not exceed {maxHours}");

            RuleFor(x => x.Priority)
                .Must(p => Enum.TryParse<PriorityEnum>(p, true, out _))
                .WithMessage($"Priority must be one of: {string.Join(", ", Enum.GetNames(typeof(PriorityEnum)))}");
        }
    }
}
