using FluentValidation;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Validators
{
    public class UpdateTaskRequestValidator : TaskRequestBaseValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidator()
        {
            RuleFor(x => x.Status)
                .Must(s => TaskStatuses.Allowed.Contains(s))
                .WithMessage($"Status must be one of: {String.Join(", ", TaskStatuses.Allowed)}");
        }
    }
}
