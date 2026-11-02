using FluentValidation;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Validators
{
    public class ProjectUpdateValidator : ProjectBaseValidator<ProjectUpdateRequest>
    {
        public ProjectUpdateValidator()
        {
            RuleFor(x => x.Status)
                .Must(s => Statuses.ProjectAllowed.Contains(s))
                .WithMessage($"Status must be one of: {String.Join(", ", Statuses.ProjectAllowed)}");

            int maxStartDays = 30;
            RuleFor(x => x.Start)
                .NotEmpty().WithMessage("Start date is required")
                .LessThan(x => x.Deadline).WithMessage("Start time cannot be later than deadline")
                .LessThan(x => DateTime.Now.AddDays(maxStartDays)).WithMessage($"Start time may not be later than {maxStartDays} days");
        }
    }
}
