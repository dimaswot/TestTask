using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }

        public List<UserFavorite> Favorites { get; set; } = new();
    }
}
