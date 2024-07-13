using FluentValidation;
using tunenest.Application.Commons;

namespace tunenest.Application.Features.Administrators.Commands.Create
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
    {
        private const int requiredLength = 8;
        private const bool requireNonAlphanumeric = true;
        private const bool requireUppercase = true;

        private readonly string messgeErrorPassword =
            $"Mật khẩu phải có ít nhất {requiredLength} ký tự, một ký tự đặc biệt, một ký tự viết hoa.";

        public CreateAdminCommandValidator()
        {
            RuleFor(p => p.user_name)
            .NotEmpty().WithMessage("user_name".ErrorEmptyProperty())
            .MaximumLength(255).WithMessage(ErrorValidationMessage.ErrorLengthProperty);

            RuleFor(p => p.email).NotEmpty().WithMessage("email".ErrorEmptyProperty())
            .MaximumLength(255).WithMessage(ErrorValidationMessage.ErrorLengthProperty)
            .EmailAddress().WithMessage("email".ErrorInvalidProperty());

            RuleFor(p => p.password)
            .NotEmpty().WithMessage("password".ErrorEmptyProperty())
            .MinimumLength(requiredLength)
            .Matches("[A-Z]").When(_ => requireUppercase)
            .Matches("[!@#$%^&*()]").When(_ => requireNonAlphanumeric).WithMessage(messgeErrorPassword);

            RuleFor(p => p.phone_number)
            .NotEmpty().WithMessage("phone_number".ErrorEmptyProperty())
            .MaximumLength(11)
            .Matches("^[0-9]*$").WithMessage("phone_number".ErrorInvalidProperty());

            RuleFor(p => p.dob)
            .NotEmpty().WithMessage("Date of birth".ErrorEmptyProperty());

            RuleFor(p => p.permission)
            .NotEmpty().WithMessage("permission".ErrorEmptyProperty());

            RuleFor(p => p.role_id)
            .NotEmpty().WithMessage("role_id".ErrorEmptyProperty());
        }
    }
}
