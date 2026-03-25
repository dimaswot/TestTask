using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrencyUpdater.Data;
using CurrencyUpdater.Entities;

using Microsoft.EntityFrameworkCore;

namespace CurrencyUpdater.Models
{

    public interface ICurrencyUpdateService
    {
        Task UpdateCurrenciesAsync(List<CbrCurrency> currencies);
    }
    public class CurrencyUpdateService : ICurrencyUpdateService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CurrencyUpdateService> _logger;

        public CurrencyUpdateService(
            ApplicationDbContext context,
            ILogger<CurrencyUpdateService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task UpdateCurrenciesAsync(List<CbrCurrency> currencies)
        {
            foreach (var currency in currencies)
            {
                // рассчитываем курс к рублю за 1 единицу валюты
                var rate = currency.Nominal > 0
                    ? currency.Value / currency.Nominal
                    : currency.Value;

                var existingCurrency = await _context.Currency
                    .FirstOrDefaultAsync(c => c.Name == currency.Name); // в иделале это делать по NumCode, но по ТЗ не храним его

                if (existingCurrency != null)
                {
                    existingCurrency.Rate = rate;
                }
                else
                {
                    _context.Currency.Add(new Currency
                    {
                        Name = currency.Name,
                        Rate = rate,
                    });
                }
            }

            await _context.SaveChangesAsync();
            // сюда логи, что добавлено/изменено успешно (не требовалось по ТЗ)
        }
    }
}
