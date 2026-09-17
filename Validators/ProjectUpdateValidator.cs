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
        }
    }
}
