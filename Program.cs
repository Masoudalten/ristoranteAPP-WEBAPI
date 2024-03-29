﻿using System.Globalization;
using apiTest.Interface;
using apiTest.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace apiTest.Classes
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddSingleton<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            //Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Application application = new Application();
            //application.Run();

        }
    }
}

