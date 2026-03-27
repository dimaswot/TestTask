using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Auth.Login
{
    internal class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand command, CancellationToken token)
        {
            var user = await _userRepository.GetByNameAsync(command.Name, token);

            if (user == null || !_passwordHasher.Verify(command.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Неверное имя или пароль");

            var jwtToken = _jwtTokenService.GenerateToken(user.Id, user.Name);

            return new LoginUserResponse(user.Id, jwtToken, "Успешный вход");
        }
    }
}
