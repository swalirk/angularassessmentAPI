using assessmentApi.Models;
using assessmentApi.services.interfaces;
using assessmentApi.services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<formDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("formCS")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddScoped<ITableInterface, TableRepository>();
builder.Services.AddScoped<IFormInterface, FormRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
