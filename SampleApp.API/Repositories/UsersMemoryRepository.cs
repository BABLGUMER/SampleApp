using SampleApp.API.Entities;
using SampleApp.API.Interfaces;

namespace SampleApp.API.Repositories;

public class UsersMemoryRepository : IUserRepository
{
    public List<User> Users { get; set; } = new List<User>();

    public User CreateUser(User user)
    {
        Users.Add(user);
        return user;
    }

    public async Task<IAsyncEnumerable<object>> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public bool DeleteUser(int id)
    {
        var result = FindUserById(id);
        Users.Remove(result);
        return true;
    }

    public async Task<bool> DeleteUserAsync(int v)
    {
        throw new NotImplementedException();
    }

    public User EditUser(User user, int id)
    {
        var result = FindUserById(id);
        result.Name = user.Name;
        return result;
    }

    public async Task EditUserAsync(User updatedUser, int v)
    {
        throw new NotImplementedException();
    }

    public User FindUserById(int id)
    {
        var result = Users.FirstOrDefault(u => u.Id == id);

        if (result == null)
        {
            throw new Exception($"Нет пользователя с id = {id}");
        }

        return result;
    }

    public async Task<IAsyncEnumerable<object>> FindUserByIdAsync(int v)
    {
        throw new NotImplementedException();
    }

    public List<User> GetUsers()
    {
        return Users;
    }

    public async Task<IAsyncEnumerable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }
}