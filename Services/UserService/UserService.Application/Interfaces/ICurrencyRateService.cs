using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Interfaces
{
    public interface ICurrencyRateService
    {
        Task<List<CurrencyRate>> GetRatesByNamesAsync(List<string> names, CancellationToken token = default);
    }

    public record CurrencyRate(string Name, decimal Rate);
}
