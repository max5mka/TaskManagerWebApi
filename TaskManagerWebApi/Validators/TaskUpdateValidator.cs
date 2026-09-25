using FluentValidation;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Validators
{
    public class TaskUpdateValidator : TaskBaseValidator<TaskUpdateRequest>
    {
        public TaskUpdateValidator()
        {
            RuleFor(x => x.Status)
                .Must(s => Statuses.TaskAllowed.Contains(s))
                .WithMessage($"Status must be one of: {String.Join(", ", Statuses.TaskAllowed)}");
        }
    }
}
