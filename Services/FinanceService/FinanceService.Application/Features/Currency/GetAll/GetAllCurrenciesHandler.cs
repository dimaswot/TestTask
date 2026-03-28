using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Application.Features.Currency.GetRates;
using FinanceService.Domain.Interfaces;

using MediatR;

namespace FinanceService.Application.Features.Currency.GetAll
{
    public class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrenciesQuery, List<CurrencyRate>>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public GetAllCurrenciesHandler (ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<List<CurrencyRate>> Handle(GetAllCurrenciesQuery query, CancellationToken cancellation = default)
        {
            var currencies = await _currencyRepository.GetAllCurrenciesAsync(cancellation);
            return currencies.Select(c => new CurrencyRate(c.Name, c.Rate)).ToList();
        }
    }
}
