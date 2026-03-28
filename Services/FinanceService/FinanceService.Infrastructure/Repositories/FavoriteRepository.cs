using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Domain.Entities;
using FinanceService.Domain.Interfaces;
using FinanceService.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteRepository(ApplicationDbContext applicationDbContext) 
        {
            _context = applicationDbContext;
        }

        public async Task<List<string>> GetUserFavoritesAsync(int userId, CancellationToken token = default)
        { 
            return await _context.UserFavorite.Where(uf  => uf.UserId == userId).Select(uf => uf.CurrencyName).ToListAsync(token);
        }

        public async Task AddFavoriteAsync(int userId, string currencyName, CancellationToken token = default)
        {
            var favorite = new UserFavorite { UserId = userId, CurrencyName = currencyName };
            await _context.UserFavorite.AddAsync(favorite, token);
            await _context.SaveChangesAsync(token);
        }
        public async Task RemoveFavoriteAsync(int userId, string currencyName, CancellationToken token = default)
        {
            var favorite = await _context.UserFavorite.FirstOrDefaultAsync(f => f.UserId == userId && f.CurrencyName == currencyName, token);

            if (favorite != null)
            {
                _context.UserFavorite.Remove(favorite);
                await _context.SaveChangesAsync(token);
            }
        }
        public async Task<bool> CheckFavoriteForUserAsync(int userId, string currencyName, CancellationToken token = default)
        {
            return await _context.UserFavorite.AnyAsync(f => f.UserId == userId && f.CurrencyName == currencyName, token);
        }
    }
}
