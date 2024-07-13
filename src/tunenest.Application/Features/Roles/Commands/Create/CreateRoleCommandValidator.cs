using FluentValidation;
using tunenest.Application.Commons;

namespace tunenest.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(p => p.name).NotEmpty().WithMessage(ErrorValidationMessage.ErrorEmptyProperty("Name"));

            RuleFor(p => p.name).MaximumLength(255).WithMessage(ErrorValidationMessage.ErrorLengthProperty);

            RuleFor(p => p.description).MaximumLength(255).WithMessage(ErrorValidationMessage.ErrorLengthProperty);
        }
    }
}
