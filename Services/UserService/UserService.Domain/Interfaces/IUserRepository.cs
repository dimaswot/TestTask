using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserService.Domain.Entities;


namespace UserService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id, CancellationToken token = default);
        Task<User?> GetByNameAsync(string name, CancellationToken token = default);
        Task<User> CreateAsync(User user, CancellationToken token = default);
        Task AddFavoriteAsync(int userId, string currencyName, CancellationToken token = default);
        Task<List<string>> GetFavoritesAsync(int userId, CancellationToken token = default);
    }
}
