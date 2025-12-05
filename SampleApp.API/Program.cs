using Microsoft.EntityFrameworkCore;
using SampleApp.API.Data;
using SampleApp.API.Interfaces;
using SampleApp.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<SampleAppContext>(o => 
    o.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

// Register repositories
builder.Services.AddScoped<IUserRepository, UsersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
