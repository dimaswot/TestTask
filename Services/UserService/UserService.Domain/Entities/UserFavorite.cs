using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class UserFavorite
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public required string CurrencyName { get; set; }
    }
}
