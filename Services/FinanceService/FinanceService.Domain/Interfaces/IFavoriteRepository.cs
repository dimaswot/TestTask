using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Domain.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<string>> GetUserFavoritesAsync(int userId, CancellationToken token = default);
        Task AddFavoriteAsync(int userId, string currencyName, CancellationToken token = default);
        Task RemoveFavoriteAsync(int userId, string currencyName, CancellationToken token = default);
        Task<bool> CheckFavoriteForUserAsync(int userId, string currencyName, CancellationToken token = default);
    }
}
