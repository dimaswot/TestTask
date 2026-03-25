using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyUpdater.Options
{
    public class CbrApiOptions
    {
        public const string ApiName = "CbrApi";
        public string BaseUrl { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 30;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(BaseUrl))
                throw new InvalidOperationException("CbrApi:BaseUrl адрес не указан");
        }
    }
}
