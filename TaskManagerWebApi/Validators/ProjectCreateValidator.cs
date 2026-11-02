using FluentValidation;
using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Validators
{
    public class ProjectCreateValidator : ProjectBaseValidator<ProjectCreateRequest> 
    {
        public ProjectCreateValidator()
        {
            int maxStartDays = 30;
            RuleFor(x => x.Start)
                .NotEmpty().WithMessage("Start date is required")
                .GreaterThan(DateTime.Now).WithMessage("Start time cannot be in the past")
                .LessThan(x => x.Deadline).WithMessage("Start time cannot be later than deadline")
                .LessThan(x => DateTime.Now.AddDays(maxStartDays)).WithMessage($"Start time may not be later than {maxStartDays} days");
        }
    }
}
