using FluentValidation;
using SampleApp.API.Entities;

namespace SampleApp.API.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин обязателен")
                .MinimumLength(3).WithMessage("Логин должен быть не менее 3 символов")
                .MaximumLength(50).WithMessage("Логин должен быть не более 50 символов");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .Length(2, 50).WithMessage("Имя должно быть от 2 до 50 символов");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .EmailAddress().WithMessage("Некорректный формат email");

            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("Возраст должен быть больше 0")
                .LessThan(150).WithMessage("Возраст должен быть меньше 150");
        }
    }
}
