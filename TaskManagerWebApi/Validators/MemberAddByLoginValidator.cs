using FluentValidation;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Validators
{
    public class MemberAddByLoginValidator : AbstractValidator<MemberAddByLoginRequest>
    {
        public MemberAddByLoginValidator()
        {
            RuleFor(x => x.Role)
                .Must(m => ProjectRoles.Roles.Contains(m))
                .WithMessage($"Role must be one of: {String.Join(", ", ProjectRoles.Roles)}");
        }
    }
}
