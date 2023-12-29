
using AutoMapper;
using Domain.Interfaces.DapperInterfaces;
using Domain.Interfaces.EfInterfaces;
using Infrastructure;
using Infrastructure.DapperRepository;
using Infrastructure.EntityFrameworkRepository;
using MainProject.Config;
using MainProject.CustomeMiddleWare;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IDapperRepository, DapperRepository>();
builder.Services.AddTransient<IEntityFrameWork, EntityFrameWorkRepository>();
builder.Services.AddAutoMapper(typeof(ProductProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseMiddleware<HandleException>();
//app.UseMiddleware<RequestAuthentication>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Product}/{action=Index}/{id?}");
   // endpoints.MapControllers();
});
app.Run();
