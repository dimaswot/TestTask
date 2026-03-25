using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyUpdater.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
