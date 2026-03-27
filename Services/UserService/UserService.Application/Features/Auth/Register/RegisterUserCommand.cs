using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace UserService.Application.Features.Auth.Register
{
    public record RegisterUserCommand(string Name, string Password)
    : IRequest<RegisterUserResponse>;

    public record RegisterUserResponse(int UserId, string Token, string Message);

}
