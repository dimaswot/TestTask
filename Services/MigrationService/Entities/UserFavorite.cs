using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationService.Entities
{
    public class UserFavorite
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public required string CurrencyName { get; set; }
        public Currency Currency { get; set; } = null!;
    }
}
