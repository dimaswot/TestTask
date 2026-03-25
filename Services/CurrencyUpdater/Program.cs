using CurrencyUpdater;
using CurrencyUpdater.Data;
using CurrencyUpdater.Models;
using CurrencyUpdater.Options;
using CurrencyUpdater.Services;
using CurrencyUpdater.Workers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("Строка подключения к БД не найдена.")));

        builder.Services.Configure<CbrApiOptions>(
            builder.Configuration.GetSection(CbrApiOptions.ApiName));

        // сервисы
        builder.Services.AddTransient<ICbrXmlParser, CbrXmlParser>();
        builder.Services.AddTransient<ICurrencyUpdateService, CurrencyUpdateService>();

        // фоновый сервис
        builder.Services.AddHostedService<CurrencyUpdaterWorker>();

        var app = builder.Build();

        var cbrOptions = app.Services.GetRequiredService<IOptions<CbrApiOptions>>().Value;
        cbrOptions.Validate();

        try
        {
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Environment.ExitCode = 1;
        }
    }
}