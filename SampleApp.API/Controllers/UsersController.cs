using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using SampleApp.API.Dto;
using SampleApp.API.Entities;
using SampleApp.API.Interfaces;
using SampleApp.API.Validations;

namespace SampleApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;
    private readonly UserValidator _validator;
    private readonly HMACSHA256 _hmac = new();

    public UsersController(IUserRepository repo)
    {
        _repo = repo;
        _validator = new UserValidator();
    }

    [HttpPost]
    public ActionResult CreateUser([FromBody] UserDto userDto)
    {
        // Создаем пользователя из DTO
        var user = new User
        {
            Login = userDto.Login,
            PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
            PasswordSalt = _hmac.Key,
            Name = userDto.Name,
            Email = userDto.Email,
            Age = userDto.Age
        };

        // Валидация
        var validationResult = _validator.Validate(user);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.First().ErrorMessage);
        }

        var createdUser = _repo.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpGet]
    public ActionResult GetUsers()
    {
        return Ok(_repo.GetUsers());
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser(int id, [FromBody] User user)
    {
        return Ok(_repo.EditUser(user, id));
    }

    [HttpGet("{id}")]
    public ActionResult GetUserById(int id)
    {
        return Ok(_repo.FindUserById(id));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        return Ok(_repo.DeleteUser(id));
    }
}
