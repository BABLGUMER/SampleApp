using Microsoft.OpenApi;
using SampleApp.API.Interfaces;
using SampleApp.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры
builder.Services.AddControllers();

// Добавляем Swagger с настройками
builder.Services.AddSwaggerGen(static c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "SampleApp API",
            Version = "v1",
            Description = "API для управления пользователями и ролями",
            Contact = new OpenApiContact
            {
                Name = "zpa",
                Email = "26ib233@git.scc",
                Url = new Uri("http://stud.scc/~26ib233/"),
            },
        }
    );
});

builder.Services.AddSingleton<IUserRepository, UsersMemoryRepository>();
builder.Services.AddSingleton<IRoleRepository, RoleMemoryRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(static c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleApp API v1");
    c.RoutePrefix = "swagger";
    c.DisplayRequestDuration();
});

app.MapControllers();

app.MapGet("/", static () => "SampleApp API работает! Используйте /swagger для документации.");
app.MapGet(
    "/health",
    static () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow })
);

app.Run();
