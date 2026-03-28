using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Application.Features.Currency.GetRates;
using FinanceService.Domain.Interfaces;

using MediatR;

namespace FinanceService.Application.Features.GetList
{
    public class GetUserFavoritesHandler : IRequestHandler<GetUserFavoriteQuery, List<string>>
    {
        private IFavoriteRepository _favoriteRepository;

        public GetUserFavoritesHandler (IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<List<string>> Handle(GetUserFavoriteQuery query, CancellationToken token)
        { 
            return await _favoriteRepository.GetUserFavoritesAsync(query.UserId, token);
        }
    }
}
