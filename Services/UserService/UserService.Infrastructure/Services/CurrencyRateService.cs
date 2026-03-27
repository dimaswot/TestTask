using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using UserService.Application.Interfaces;
using UserService.Infrastructure.Data;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly ApplicationDbContext _context;

        public CurrencyRateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CurrencyRate>> GetRatesByNamesAsync(List<string> names, CancellationToken token = default)
        {
            return await _context.Set<Currency>()
                .Where(c => names.Contains(c.Name))
                .Select(c => new CurrencyRate(c.Name, c.Rate))
                .ToListAsync(token);
        }
    }
}
