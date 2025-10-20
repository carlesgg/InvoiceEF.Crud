using HealthChecks.UI.Client;
using InvoiceEF.Crud.Api.HealthChecks;
using InvoiceEF.Crud.Application.Services.Extensions;
using InvoiceEF.Crud.Infrastructure.Proxies.Extensions;
using InvoiceEF.Crud.Infrastructure.Repositories.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddProxies();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map HealthChecks endpoints + UI
app.MapHealthChecksUISetup();

app.Run();
