using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Domain.Interfaces;

using MediatR;

namespace FinanceService.Application.Features.Currency.GetRates
{
    public class GetUserCurrencyRatesHandler : IRequestHandler<GetUserCurrencyRatesQuery, List<CurrencyRate>>
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public GetUserCurrencyRatesHandler(
            IFavoriteRepository favoriteRepository,
            ICurrencyRepository currencyRepository)
        {
            _favoriteRepository = favoriteRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<List<CurrencyRate>> Handle(GetUserCurrencyRatesQuery query, CancellationToken token)
        {
            // Получаем избранные валюты пользователя
            var favoriteNames = await _favoriteRepository.GetUserFavoritesAsync(query.UserId, token);

            if (!favoriteNames.Any())
                return new List<CurrencyRate>();

            // Получаем курсы для избранных валют
            var currencies = await _currencyRepository.GetCurrenciesByNamesAsync(favoriteNames, token);

            return currencies.Select(c => new CurrencyRate(c.Name, c.Rate)).ToList();
        }
    }
}
