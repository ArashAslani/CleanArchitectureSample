using Common.Application.Validation;
using FluentValidation;
using UM.Application.Users.Create;

namespace UM.Application.Users.Register;

internal class RegisterUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    public RegisterUserCommandValidation()
    {

        RuleFor(f => f.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("Password"))
            .NotNull().WithMessage(ValidationMessages.required("Password"))
            .MinimumLength(4).WithMessage("Password must be greater than 4 characters.");
    }
}