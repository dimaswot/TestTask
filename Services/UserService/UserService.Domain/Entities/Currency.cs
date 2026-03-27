using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
