using EnergyCompany.WebAPI.Context;
using EnergyCompany.WebAPI.Context.Repositories;
using EnergyCompany.WebAPI.Interfaces.Repositories;
using EnergyCompany.WebAPI.Interfaces.Services;
using EnergyCompany.WebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EnergyCompanyContext>(options => options.UseInMemoryDatabase("Database"));
builder.Services.AddScoped<IEndpointRepository, EndpointsRepository>();
builder.Services.AddScoped<IEndpointService, EndpointService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
