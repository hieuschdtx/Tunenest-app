using FluentValidation;
using tunenest.Application.Commons;

namespace tunenest.Application.Features.Administrators.Commands.Login
{
    public class LoginAdministratorCommandValidator : AbstractValidator<LoginAdministratorCommand>
    {
        public LoginAdministratorCommandValidator()
        {
            RuleFor(p => p.email)
            .NotEmpty().WithMessage("email".ErrorEmptyProperty())
            .EmailAddress().WithMessage("email".ErrorInvalidProperty());

            RuleFor(p => p.password)
                .NotEmpty().WithMessage("password".ErrorEmptyProperty());
        }
    }
}
