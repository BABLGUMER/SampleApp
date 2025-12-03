using FluentValidation.TestHelper;
using SampleApp.API.Entities;
using SampleApp.API.Validations;

namespace SampleApp.API.Tests.Validations;

public class UserValidatorTests
{
    private readonly UserValidator _validator;

    public UserValidatorTests()
    {
        _validator = new UserValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new User { Id = 1, Name = "" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(user => user.Name).WithErrorMessage("Имя обязательно");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Short()
    {
        var model = new User { Id = 1, Name = "А" };
        var result = _validator.TestValidate(model);
        result
            .ShouldHaveValidationErrorFor(user => user.Name)
            .WithErrorMessage("Имя должно быть от 2 до 50 символов");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Starts_With_Lowercase()
    {
        var model = new User { Id = 1, Name = "иван" };
        var result = _validator.TestValidate(model);
        result
            .ShouldHaveValidationErrorFor(user => user.Name)
            .WithErrorMessage("Имя должно начинаться с заглавной буквы");
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Negative()
    {
        var model = new User { Id = -1, Name = "Иван" };
        var result = _validator.TestValidate(model);
        result
            .ShouldHaveValidationErrorFor(user => user.Id)
            .WithErrorMessage("ID должен быть положительным числом");
    }

    [Fact]
    public void Should_Not_Have_Error_When_User_Is_Valid()
    {
        var model = new User { Id = 1, Name = "Иван" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
