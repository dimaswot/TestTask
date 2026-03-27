using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace UserService.Application.Features.GetUserRates
{
    public record GetUserCurrencyRatesQuery(int UserId) : IRequest<List<CurrencyRate>>;

    public record CurrencyRate(string Name, decimal Rate);
}
