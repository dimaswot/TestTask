using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace UserService.Application.Features.Auth.Login
{
    public record LoginUserCommand(string Name, string Password) : IRequest<LoginUserResponse>;

    public record LoginUserResponse(int UserId, string Token, string Message);
}


