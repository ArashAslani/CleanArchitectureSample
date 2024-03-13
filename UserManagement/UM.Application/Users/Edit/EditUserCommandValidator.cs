using Common.Application.Validation;
using FluentValidation;

namespace UM.Application.Users.Edit;

public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(r => r.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("Email is invalid.");


        RuleFor(f => f.Avatar)
            .JustImageFile();
    }
}