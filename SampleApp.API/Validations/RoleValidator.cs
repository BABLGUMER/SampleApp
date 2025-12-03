using FluentValidation;
using SampleApp.API.Entities;

namespace SampleApp.API.Validations;

public class RoleValidator : AbstractValidator<Role>
{
    public RoleValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Название роли обязательно")
            .Length(2, 50)
            .WithMessage("Название роли должно быть от 2 до 50 символов")
            .Must(StartsWithCapitalLetter)
            .WithMessage("Название роли должно начинаться с заглавной буквы");

        RuleFor(r => r.Id).GreaterThan(0).WithMessage("ID должен быть положительным числом");

        RuleFor(r => r.Description)
            .MaximumLength(200)
            .WithMessage("Описание не должно превышать 200 символов");
    }

    private bool StartsWithCapitalLetter(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;
        return char.IsUpper(name[0]);
    }
}
