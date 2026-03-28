using FinanceService.Application.Features.Currency.GetAll;
using FinanceService.Application.Features.Currency.GetRates;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить курсы валют из избранного пользователя
        /// </summary>
        [HttpGet("rates")]
        public async Task<IActionResult> GetUserRates()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rates = await _mediator.Send(new GetUserCurrencyRatesQuery(userId));
            return Ok(rates);
        }

        /// <summary>
        /// Получить все валюты
        /// </summary>
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var rates = await _mediator.Send(new GetAllCurrenciesQuery());
            return Ok(rates);
        }
    }
}
