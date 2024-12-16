using FluentValidation;
using tunenest.Application.Commons;

namespace tunenest.Application.Features.Roles.Commands.Update
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.name)
                .MaximumLength(255).WithMessage(ErrorValidationMessage.ErrorLengthProperty);

            RuleFor(x => x.description)
                .MaximumLength(255).WithMessage(ErrorValidationMessage.ErrorLengthProperty);
        }
    }
}
