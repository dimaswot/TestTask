using FinanceService.Application.Features.Favorite.Add;
using FinanceService.Application.Features.Favorite.Remove;
using FinanceService.Application.Features.GetList;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FavoritesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список избранных валют пользователя
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var favorites = await _mediator.Send(new GetUserFavoriteQuery(userId));
            return Ok(favorites);
        }

        /// <summary>
        /// Добавить валюту в избранное пользователю
        /// </summary>
        [HttpPost("{currencyName}")]
        public async Task<IActionResult> AddFavorite(string currencyName)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var response = await _mediator.Send(new AddFavoriteCommand(userId, currencyName));
            return Ok(response);
        }

        /// <summary>
        /// Удалить валюту из избранного у пользователя
        /// </summary>
        [HttpDelete("{currencyName}")]
        public async Task<IActionResult> RemoveFavorite(string currencyName)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var response = await _mediator.Send(new RemoveFavoriteCommand(userId, currencyName));
            return Ok(response);
        }
    }
}
