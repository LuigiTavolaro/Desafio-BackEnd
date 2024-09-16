using DesafioBackEndProject.Application;
using DesafioBackEndProject.DependecyInjection;
using DesafioBackEndProject.Domain.Common;
using DesafioBackEndProject.Infrastructure;
using DesafioBackEndProject.Infrastructure.Mapping;
using DesafioBackEndProject.Middlewares;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationConfiguration();
builder.Services.AddMassTransitConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddSingleton<NotificationHandler>();

builder.Services.AddMemoryCache();

var config = TypeAdapterConfig.GlobalSettings;
MappingConfig.ConfigureMappings();

// Registra o TypeAdapterConfig como Singleton
builder.Services.AddSingleton(config);
builder.Services.AddSingleton<IMapper, ServiceMapper>();




// Registro dos serviços
builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
