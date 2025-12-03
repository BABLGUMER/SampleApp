using SampleApp.API.Entities;
using SampleApp.API.Repositories;

namespace SampleApp.API.Tests.Repositories;

public class UsersMemoryRepositorySyncTests
{
    private readonly UsersMemoryRepository _repository;

    public UsersMemoryRepositorySyncTests()
    {
        _repository = new UsersMemoryRepository();
    }

    [Fact]
    public void CreateUser_Should_Add_User_To_List()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Иван" };

        // Act
        var result = _repository.CreateUser(user);

        // Assert
        Assert.Equal(user, result);
        Assert.Single(_repository.Users);
        Assert.Contains(user, _repository.Users);
    }

    [Fact]
    public void GetUsers_Should_Return_All_Users()
    {
        // Arrange
        var user1 = new User { Id = 1, Name = "Иван" };
        var user2 = new User { Id = 2, Name = "Петр" };

        // Act
        _repository.CreateUser(user1);
        _repository.CreateUser(user2);

        var users = _repository.GetUsers();

        // Assert
        Assert.Equal(2, users.Count);
        Assert.Contains(user1, users);
        Assert.Contains(user2, users);
    }

    [Fact]
    public void FindUserById_Should_Return_Correct_User()
    {
        // Arrange
        var user1 = new User { Id = 1, Name = "Иван" };
        var user2 = new User { Id = 2, Name = "Петр" };

        // Act
        _repository.CreateUser(user1);
        _repository.CreateUser(user2);

        var result = _repository.FindUserById(2);

        // Assert
        Assert.Equal(user2, result);
    }

    [Fact]
    public void FindUserById_Should_Throw_Exception_When_User_Not_Found()
    {
        // Act & Assert
        Assert.Throws<Exception>(() => _repository.FindUserById(999));
    }

    [Fact]
    public void EditUser_Should_Update_User_Name()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Иван" };
        _repository.CreateUser(user);

        var updatedUser = new User { Id = 1, Name = "Иван Петров" };

        // Act
        var result = _repository.EditUser(updatedUser, 1);

        // Assert
        Assert.Equal("Иван Петров", result.Name);
        Assert.Equal("Иван Петров", _repository.Users[0].Name);
    }

    [Fact]
    public void DeleteUser_Should_Remove_User_From_List()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Иван" };
        _repository.CreateUser(user);

        // Act
        var result = _repository.DeleteUser(1);

        // Assert
        Assert.True(result);
        Assert.Empty(_repository.Users);
    }
}
