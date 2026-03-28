using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace FinanceService.Application.Features.Favorite.Remove
{
    public record RemoveFavoriteCommand(int UserId, string CurrencyName) : IRequest<RemoveFavoriteResponse>;

    public record RemoveFavoriteResponse(string Message, string CurrencyName);
}
