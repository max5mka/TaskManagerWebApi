using FluentValidation;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Validators
{
    public class UpdateProjectRequestValidator : ProjectRequestBaseValidator<UpdateProjectRequest>
    {
        public UpdateProjectRequestValidator()
        {
            RuleFor(x => x.Status)
                .Must(s => ProjectStatuses.Allowed.Contains(s))
                .WithMessage($"Status must be one of: {String.Join(", ", ProjectStatuses.Allowed)}");
        }
    }
}
