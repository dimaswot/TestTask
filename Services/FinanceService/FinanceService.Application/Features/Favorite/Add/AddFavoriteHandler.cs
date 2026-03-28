using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Domain.Interfaces;

using MediatR;

namespace FinanceService.Application.Features.Favorite.Add
{
    public class AddFavoriteHandler : IRequestHandler<AddFavoriteCommand ,AddFavoriteResponse>
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public AddFavoriteHandler(
            IFavoriteRepository favoriteRepository,
            ICurrencyRepository currencyRepository)
        {
            _favoriteRepository = favoriteRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<AddFavoriteResponse> Handle(AddFavoriteCommand command, CancellationToken token)
        {
            // Проверяем, существует ли валюта
            var currency = await _currencyRepository.GetCurrencyByNameAsync(command.CurrencyName, token);
            if (currency == null)
                throw new InvalidOperationException($"Такая валюта '{command.CurrencyName}' не найдена");

            // Проверяем, не добавлена ли уже
            var isFavorite = await _favoriteRepository.CheckFavoriteForUserAsync(command.UserId, currency.Name, token);
            if (isFavorite)
                throw new InvalidOperationException($"Такая валюта '{currency.Name}' уже есть в избранном");

            // Добавляем в избранное
            await _favoriteRepository.AddFavoriteAsync(command.UserId, currency.Name, token);

            return new AddFavoriteResponse("В избранное добавлена новая валюта", currency.Name);
        }
    }
}
