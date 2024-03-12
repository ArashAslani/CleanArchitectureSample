using Common.Application.Validation;
using FluentValidation;
using UM.Application.UserApplication.Register;

namespace UM.Application.UserApplication.Login;

internal class LoginUserCommandValidation : AbstractValidator<RegisterUserCommand>
{
    public LoginUserCommandValidation()
    {

        RuleFor(f => f.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("Password"))
            .NotNull().WithMessage(ValidationMessages.required("Password"))
            .MinimumLength(4).WithMessage("Password must be greater than 4 characters.");
    }
}