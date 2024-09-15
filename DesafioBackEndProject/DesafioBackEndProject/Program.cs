using DesafioBackEndProject.Application;
using DesafioBackEndProject.Application.Validators;
using DesafioBackEndProject.Infrastructure;
using DesafioBackEndProject.Infrastructure.Data;
using DesafioBackEndProject.Infrastructure.Mapping;
using DesafioBackEndProject.Infrastructure.Messaging;
using DesafioBackEndProject.Middlewares;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<MotorcycleValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DriverValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RentalValidator>();

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<DesafioDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions
            .EnableRetryOnFailure() // Configura retry em caso de falha
            .CommandTimeout(60) // Tempo limite para comandos
            .MinBatchSize(1) // Configura o tamanho mínimo do lote
            .MaxBatchSize(10)) // Configura o tamanho máximo do lote
);

var config = TypeAdapterConfig.GlobalSettings;
MappingConfig.ConfigureMappings();

// Registra o TypeAdapterConfig como Singleton
builder.Services.AddSingleton(config);
builder.Services.AddSingleton<IMapper, ServiceMapper>();



builder.Services.AddMassTransit(x =>
{
     x.AddConsumer<MotorcycleConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost:5672"), h =>
        {
            h.Username("guest");  // Credenciais definidas no docker-compose.yml
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("motorcycle-queue", e =>
        {
            e.ConfigureConsumer<MotorcycleConsumer>(context);
        });

    });
});

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
