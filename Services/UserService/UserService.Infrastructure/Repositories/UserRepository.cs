using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _context.Users.FindAsync(new object[] { id }, token);
        }
        public async Task<User?> GetByNameAsync(string name, CancellationToken token = default)
        {
            return await _context.Users.FirstOrDefaultAsync( u  => u.Name == name, token);
        }
        public async Task<User> CreateAsync(User user, CancellationToken token = default)
        {
            await _context.Users.AddAsync(user, token);
            await _context.SaveChangesAsync(token);
            return user;
        }
        public async Task AddFavoriteAsync(int userId, string currencyName, CancellationToken token = default)
        {
            var favorite = new UserFavorite { UserId = userId, CurrencyName = currencyName };
            await _context.UserFavorite.AddAsync(favorite, token);
            await _context.SaveChangesAsync(token);
        }
        public async Task<List<string>> GetFavoritesAsync(int userId, CancellationToken token = default)
        { 
            return await _context.UserFavorite.Where( uf => uf.UserId == userId).Select(uf => uf.CurrencyName).ToListAsync(token);
        }
    }
}
