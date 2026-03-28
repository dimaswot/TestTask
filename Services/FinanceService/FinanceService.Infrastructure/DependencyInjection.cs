using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Domain.Interfaces;
using FinanceService.Infrastructure.Data;
using FinanceService.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("Default")));

            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();

            return services;
        }
    }
}
