using Common.Application.Validation;
using FluentValidation;

namespace UM.Application.Users.Create;    

internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(r => r.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(f => f.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("Password"))
            .NotNull().WithMessage(ValidationMessages.required("Password"))
            .MinimumLength(4).WithMessage("Password must be greater than 4 characters.");
    }
}