using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace UserService.Application.Features.Auth.Logout
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand, LogoutUserResponse>
    {
        public Task<LogoutUserResponse> Handle(LogoutUserCommand command, CancellationToken token)
        {
            return Task.FromResult(new LogoutUserResponse("Выход выполнен успешно"));
        }
    }
}
