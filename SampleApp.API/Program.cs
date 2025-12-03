using SampleApp.API.Interfaces;
using SampleApp.API.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IUserRepository, UsersMemoryRepository>();
        builder.Services.AddSingleton<IRoleRepository, RoleMemoryRepository>();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }
}
