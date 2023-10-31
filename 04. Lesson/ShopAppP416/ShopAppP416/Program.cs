using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ShopAppP416;
using ShopAppP416.Data;
using ShopAppP416.Dtos.ProductDtos;
using ShopAppP416.Mapper;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.RegisterService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
