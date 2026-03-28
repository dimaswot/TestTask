using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace FinanceService.Application.Features.Currency.GetRates
{
    public record GetUserCurrencyRatesQuery(int UserId) : IRequest<List<CurrencyRate>>;

    public record CurrencyRate(string Name, decimal Rate);
}
