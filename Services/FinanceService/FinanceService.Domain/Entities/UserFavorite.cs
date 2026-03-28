using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Domain.Entities
{
    public class UserFavorite
    {
        public int UserId { get; set; }
        public required string CurrencyName { get; set; }
    }
}
