using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MigrationService.Data;
using MigrationService.Entities;

namespace MigrationService;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("Строка подключения к БД не найдена.")));

        var app = builder.Build();

        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            Environment.ExitCode = 0;
        }
        catch (Exception ex)
        {
            // тут вывод ошибки в логи, не добавил, не было требований
            Environment.ExitCode = 1;
        }
    }
}