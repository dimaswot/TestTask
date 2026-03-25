using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurrencyUpdater.Models
{
    public class CbrCurrency
    {
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public decimal Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal VunitRate { get; set; }
    }
}
