using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainTicketMachine.Api.Services;
using TrainTicketMachine.Infrastructure.Repositories;
using TrainTicketMachine.Infrastructure.Services;

namespace TrainTicketMachine.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddScoped<IStationsRepository, StationsRepository>();
        services.AddScoped<IStationsService, StationsService>();
        services.AddScoped<ITrainTicketService, TrainTicketService>();

        var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .AddJsonFile("appsettings.json")
        .Build();

        services.AddSingleton<IConfiguration>(configuration);

        var serviceProvider = services.BuildServiceProvider();

        var host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder =>
        {
            builder.Sources.Clear();
            builder.AddConfiguration(configuration);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .Build();

        var uri = configuration.GetValue<string>("CentralSystem");

        var appService = serviceProvider.GetService<ITrainTicketService>();
        appService.Run(uri);
    }
}