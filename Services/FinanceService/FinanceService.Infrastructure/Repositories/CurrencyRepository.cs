using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Domain.Entities;
using FinanceService.Domain.Interfaces;
using FinanceService.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Repositories
{
    public class CurrencyRepository:ICurrencyRepository
    {
        private readonly ApplicationDbContext _context;

        public CurrencyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync(CancellationToken token = default)
        {
            return await _context.Currency.ToListAsync(token);
        }
        public async Task<List<Currency>> GetCurrenciesByNamesAsync(List<string> names, CancellationToken token = default)
        { 
            return await _context.Currency.Where(c=> names.Contains(c.Name)).ToListAsync();
        }
        public async Task<Currency?> GetCurrencyByNameAsync(string name, CancellationToken token = default)
        {
            return await _context.Currency.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
