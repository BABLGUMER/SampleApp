using Microsoft.EntityFrameworkCore;
using Npgsql;
using SampleApp.API.Data;
using SampleApp.API.Entities;
using SampleApp.API.Interfaces;

namespace SampleApp.API.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly SampleAppContext _db;

        public UsersRepository(SampleAppContext db)
        {
            _db = db;
        }

        public List<User> GetUsers()
        {
            return _db.Users.ToList();
        }

        public User CreateUser(User user)
        {
            try
            {
                _db.Add(user);
                _db.SaveChanges();
                return user;
            }
            catch (NpgsqlException ex)
            {
                throw new NpgsqlException($"Ошибка SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }

        public User EditUser(User user, int id)
        {
            var existingUser = _db.Users.Find(id);
            if (existingUser == null)
                throw new Exception($"Пользователь с id {id} не найден");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Age = user.Age;
            existingUser.UpdatedAt = DateTime.UtcNow;
            
            _db.SaveChanges();
            return existingUser;
        }

        public bool DeleteUser(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
                return false;

            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }

        public User FindUserById(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
                throw new Exception($"Пользователь с id {id} не найден");
            
            return user;
        }
    }
}
