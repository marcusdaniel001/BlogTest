using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDev")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ServicesInjection(builder);

RepositoriesInjection(builder);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void ServicesInjection(WebApplicationBuilder builder)
{
    //Dependency injection for services
    builder.Services.AddTransient<IBlogService, BlogService>();
}

static void RepositoriesInjection(WebApplicationBuilder builder)
{
    //Dependency Injection for repositories
    builder.Services.AddTransient<IBlogRepository, BlogRepository>();
}