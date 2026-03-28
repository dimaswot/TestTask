using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace FinanceService.Application.Features.GetList
{
    public record GetUserFavoriteQuery (int UserId) : IRequest<List<string>>;
}
