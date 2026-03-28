using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Application.Features.Currency.GetRates;

using MediatR;

namespace FinanceService.Application.Features.Currency.GetAll
{
    public record GetAllCurrenciesQuery() : IRequest<List<CurrencyRate>>;
}
