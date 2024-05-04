using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.Values;
using System.IO;
using WebApplication2;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = new WebHostBuilder();
        builder.UseKestrel();
        builder.UseContentRoot(Directory.GetCurrentDirectory());
        
        builder.ConfigureAppConfiguration((hostingContext, config) =>
           {
               config
                   .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   //.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                   .AddJsonFile("ocelot.json")
                   .AddEnvironmentVariables();
           });

        builder.ConfigureLogging((hostingContext, logging) =>
           {
               if (hostingContext.HostingEnvironment.IsDevelopment())
               {
                   logging.ClearProviders();
                   logging.AddConsole();
               }
               //add your logging
           });

        

        builder.UseIISIntegration();
        builder.UseStartup<Startup>();

       

        var app = builder.Build();

        app.Run();
    }
}