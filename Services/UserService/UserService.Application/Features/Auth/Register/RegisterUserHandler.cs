using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Auth.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public RegisterUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand command, CancellationToken token)
        {
            // Проверка на существующего пользователя
            var existing = await _userRepository.GetByNameAsync(command.Name, token);
            if (existing != null)
                throw new InvalidOperationException($"Пользователь '{command.Name}' уже существует");

            // Создание пользователя
            var user = new User
            {
                Name = command.Name,
                PasswordHash = _passwordHasher.Hash(command.Password)
            };

            var created = await _userRepository.CreateAsync(user, token);
            var jwtToken = _jwtTokenService.GenerateToken(created.Id, created.Name);

            return new RegisterUserResponse(created.Id, jwtToken, "Пользователь успешно зарегистрирован");
        }
    }
}
