using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Services;

namespace Services.Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope=host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
                if (!categoryService.GetAllAsync().Result.Data.Any())
                {
                    categoryService.CreateAsync(new CategoryForCreateDto { Name = "Asp.Net.Core" }).Wait();
                    categoryService.CreateAsync(new CategoryForCreateDto { Name = "Java" }).Wait();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
