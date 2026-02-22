using Microsoft.AspNetCore.Mvc;
using MinecraftSkins.Services.Interfaces.IServices;

namespace MinecraftSkins.Controllers
{   
    [Route("api/exchange-rate")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var rate = await _exchangeRateService.GetRateAsync(cancellationToken);
            return Ok(rate);
        }
    }
}
