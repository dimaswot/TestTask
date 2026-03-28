using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Domain.Interfaces;

using MediatR;

namespace FinanceService.Application.Features.Favorite.Remove
{
    public class RemoveFavoriteHandler : IRequestHandler<RemoveFavoriteCommand, RemoveFavoriteResponse>
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public RemoveFavoriteHandler(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<RemoveFavoriteResponse> Handle(RemoveFavoriteCommand command, CancellationToken cancellationToken)
        {
            await _favoriteRepository.RemoveFavoriteAsync(command.UserId, command.CurrencyName, cancellationToken);
            return new RemoveFavoriteResponse("Из избранного удалена валюьа", command.CurrencyName);
        }
    }
}
