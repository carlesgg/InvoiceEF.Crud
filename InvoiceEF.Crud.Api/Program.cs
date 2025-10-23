using FluentValidation;
using InvoiceEF.Crud.Api.HealthChecks;
using InvoiceEF.Crud.Api.Middlewares;
using InvoiceEF.Crud.Application.Services.Extensions;
using InvoiceEF.Crud.Infrastructure.Proxies.Extensions;
using InvoiceEF.Crud.Infrastructure.Repositories.Extensions;
using Serilog;
using System.ComponentModel.DataAnnotations;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // optional: from appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

// Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<ClientCreateDtoValidator>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddProxies(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

// Register HealthChecks UI
builder.Services.AddHealthChecksUISetup();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map HealthChecks endpoints + UI
app.MapHealthChecksUISetup();

app.Run();
