using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Bogus;
using SampleApp.API.Data;
using SampleApp.API.Dto;
using SampleApp.API.Entities;

namespace SampleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly SampleAppContext _db;

        public SeedController(SampleAppContext db)
        {
            _db = db;
        }

        [HttpGet("generate")]
        public ActionResult SeedUsers()
        {
            using var hmac = new HMACSHA256();

            Faker<UserDto> _faker = new Faker<UserDto>("en")
                .RuleFor(u => u.Login, f => GenerateLogin(f).Trim())
                .RuleFor(u => u.Password, f => GeneratePassword(f).Trim().Replace(" ", ""))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Age, f => f.Random.Int(18, 65));

            string GenerateLogin(Faker faker)
            {
                return faker.Random.Word() + faker.Random.Number(3, 5);
            }

            string GeneratePassword(Faker faker)
            {
                return faker.Random.Word() + faker.Random.Number(3, 5);
            }

            var users = _faker.Generate(100).Where(u => u.Login.Length > 4 && u.Login.Length <= 10);

            List<User> userToDb = new List<User>();

            try
            {
                foreach (var user in users)
                {
                    var u = new User()
                    {
                        Login = user.Login,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                        PasswordSalt = hmac.Key,
                        Name = user.Name,
                        Email = user.Email,
                        Age = user.Age
                    };
                    userToDb.Add(u);
                }
                _db.Users.AddRange(userToDb);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.InnerException!.Message}");
                return BadRequest($"Ошибка: {ex.Message}");
            }

            return Ok(new { 
                message = "Тестовые данные созданы", 
                count = userToDb.Count 
            });
        }
    }
}
