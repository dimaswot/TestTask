using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace FinanceService.Application.Features.Favorite.Add
{
    public record AddFavoriteCommand(int UserId, string CurrencyName) : IRequest<AddFavoriteResponse>;

    public record AddFavoriteResponse(string Message, string CurrencyName);
}
