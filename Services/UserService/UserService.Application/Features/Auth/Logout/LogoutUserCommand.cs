using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;


namespace UserService.Application.Features.Auth.Logout
{
    public record LogoutUserCommand(int UserId) : IRequest<LogoutUserResponse>;
    public record LogoutUserResponse(string Message);
}
