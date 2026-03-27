using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using UserService.Application.Interfaces;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.GetUserRates
{
    public class GetUserCurrencyRatesHandler : IRequestHandler<GetUserCurrencyRatesQuery, List<CurrencyRate>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRateService _currencyRateService;

        public GetUserCurrencyRatesHandler(
            IUserRepository userRepository,
            ICurrencyRateService currencyRateService)
        {
            _userRepository = userRepository;
            _currencyRateService = currencyRateService;
        }

        public async Task<List<CurrencyRate>> Handle(GetUserCurrencyRatesQuery query, CancellationToken token)
        {
            var favorites = await _userRepository.GetFavoritesAsync(query.UserId, token);

            if (favorites == null || !favorites.Any())
                return new List<CurrencyRate>();

            var rates = await _currencyRateService.GetRatesByNamesAsync(favorites, token);

            return rates.Select(r => new CurrencyRate(r.Name, r.Rate)).ToList();
        }
    }
}
