using Common.Application.Validation;
using FluentValidation;

namespace UM.Application.RoleApplication.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty().WithMessage(ValidationMessages.required("Title"));
    }
}