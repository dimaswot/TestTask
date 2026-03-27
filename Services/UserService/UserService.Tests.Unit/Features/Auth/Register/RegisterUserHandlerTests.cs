using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using Xunit;
using FluentAssertions;

using UserService.Application.Features.Auth.Register;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Tests.Unit.Features.Auth.Register
{
    public class RegisterUserHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IJwtTokenService> _mockJwtService;
        private readonly RegisterUserHandler _handler;

        public RegisterUserHandlerTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockJwtService = new Mock<IJwtTokenService>();

            _handler = new RegisterUserHandler(
                _mockUserRepo.Object,
                _mockPasswordHasher.Object,
                _mockJwtService.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResponse()
        {
            var command = new RegisterUserCommand("testuser", "Password1");
            _mockUserRepo.Setup(r => r.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);
            _mockUserRepo.Setup(r => r.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 1, Name = "testuser", PasswordHash = "hash"});
            _mockJwtService.Setup(s => s.GenerateToken(It.IsAny<int>(), It.IsAny<string>()))
                .Returns("asysaysaysay-token");

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.UserId.Should().Be(1);
            result.Token.Should().Be("asysaysaysay-token");
            _mockUserRepo.Verify(r => r.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DuplicateUser_ThrowsException()
        {
            var command = new RegisterUserCommand("existinguser", "Password123");
            _mockUserRepo.Setup(r => r.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Id = 1, Name = "existinguser", PasswordHash = "hash" });

            await FluentActions.Invoking(() => _handler.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*уже существует*");
        }
    }
}
