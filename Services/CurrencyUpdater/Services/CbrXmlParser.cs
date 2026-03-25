using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using CurrencyUpdater.Models;

namespace CurrencyUpdater.Services
{
    public interface ICbrXmlParser
    {
        Task<List<CbrCurrency>> ParseAsync(string xmlContent);
    }

    public class CbrXmlParser : ICbrXmlParser
    {
        public Task<List<CbrCurrency>> ParseAsync(string xmlContent)
        {
            var currencies = new List<CbrCurrency>();

            var doc = XDocument.Parse(xmlContent);
            var root = doc.Element("ValCurs");

            if (root == null)
                throw new Exception("Неверный формат XML от ЦБ РФ");

            var dateAttr = root.Attribute("Date");
            var currentDate = dateAttr != null ? DateTime.Parse(dateAttr.Value) : DateTime.Today;

            foreach (var valute in root.Elements("Valute"))
            {
                var numCode = valute.Element("NumCode")?.Value ?? string.Empty;
                var charCode = valute.Element("CharCode")?.Value ?? string.Empty;
                var name = valute.Element("Name")?.Value ?? string.Empty;
                var nominalStr = valute.Element("Nominal")?.Value ?? "1";
                var valueStr = valute.Element("Value")?.Value ?? "0";
                var vunitRateStr = valute.Element("VunitRate")?.Value ?? "0";

                if (decimal.TryParse(nominalStr, out var nominal) &&
                    decimal.TryParse(valueStr, out var value) &&
                    decimal.TryParse(vunitRateStr, out var vunitRate))
                {
                    currencies.Add(new CbrCurrency
                    {
                        NumCode = numCode,
                        CharCode = charCode,
                        Name = name,
                        Nominal = nominal,
                        Value = value,
                        VunitRate = vunitRate
                    });
                }
            }

            return Task.FromResult(currencies);
        }
    }
}
