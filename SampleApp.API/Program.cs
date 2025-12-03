using Microsoft.OpenApi;
using SampleApp.API.Interfaces;
using SampleApp.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
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
                Url = new Uri("http://stud.scc/~26ib233"),
            },
        }
    );
});

builder.Services.AddSingleton<IUserRepository, UsersMemoryRepository>();
builder.Services.AddSingleton<IRoleRepository, RoleMemoryRepository>();

var app = builder.Build();

app.UseCors("AllowAngularApp");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleApp API v1");
    c.RoutePrefix = "swagger";
    c.DisplayRequestDuration();
});

app.MapControllers();

app.Run();
