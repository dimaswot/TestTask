using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using FinanceService.Application.Features.Currency.GetRates;
using FinanceService.Domain.Interfaces;

using FluentAssertions;

using Moq;

namespace FinanceService.Tests.Unit.Features.Currency.GetRates
{
    public class GetUserCurrencyRatesHandlerTests
    {
        private readonly Mock<IFavoriteRepository> _mockFavoriteRepository;
        private readonly Mock<ICurrencyRepository> _mockCurrenctRepository;
        private readonly GetUserCurrencyRatesHandler _handler;

        public GetUserCurrencyRatesHandlerTests (Mock<IFavoriteRepository> mockFavoriteRepository, Mock<ICurrencyRepository> mockCurrenctRepository, GetUserCurrencyRatesHandler userCurrencyRatesHandler)
        {
            _mockFavoriteRepository = mockFavoriteRepository;
            _mockCurrenctRepository = mockCurrenctRepository;
            _handler = userCurrencyRatesHandler;
        }

        [Fact]
        public async Task Handle_UserHasFavorites_ReturnsCurrencyRates()
        {
            // Arrange
            var query = new GetUserCurrencyRatesQuery(1);
            var favorites = new List<string> { "Доллар США", "Евро" };
            var currencies = new List<Domain.Entities.Currency>
        {
            new() { Id = 1, Name = "Доллар США", Rate = 80.96m, },
            new() { Id = 2, Name = "Евро", Rate = 93.92m,}
        };

            _mockFavoriteRepository.Setup(r => r.GetUserFavoritesAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(favorites);
            _mockCurrenctRepository.Setup(r => r.GetCurrenciesByNamesAsync(favorites, It.IsAny<CancellationToken>()))
                .ReturnsAsync(currencies);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().HaveCount(2);
            result.Should().Contain(r => r.Name == "Доллар США");
            result.Should().Contain(r => r.Name == "Евро");
        }

        [Fact]
        public async Task Handle_UserHasNoFavorites_ReturnsEmptyList()
        {
            var query = new GetUserCurrencyRatesQuery(1);
            _mockFavoriteRepository.Setup(r => r.GetUserFavoritesAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<string>());

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().BeEmpty();
        }

    }
}
