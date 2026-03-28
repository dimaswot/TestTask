using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Domain.Entities;

namespace FinanceService.Domain.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetAllCurrenciesAsync(CancellationToken token = default);
        Task<List<Currency>> GetCurrenciesByNamesAsync(List<string> names, CancellationToken token = default);
        Task<Currency?> GetCurrencyByNameAsync(string name, CancellationToken token = default);
    }
}
