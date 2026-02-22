using Microsoft.AspNetCore.Mvc;
using MinecraftSkins.Services.Interfaces.IServices;
using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Controllers
{
    [Route("api/rates")]
    [ApiController]
    public class RateDiagnosticsController : ControllerBase
    {
        private readonly IRateDiagnosticsService _diagnosticsService;

        public RateDiagnosticsController(IRateDiagnosticsService diagnosticsService)
        {
            _diagnosticsService = diagnosticsService;
        }

        [HttpGet("btc-usd")]
        public async Task<ActionResult<RateDiagnosticsResponse>> GetStatus(CancellationToken cancellationToken)
        {
            var result = await _diagnosticsService.GetDiagnosticsAsync(cancellationToken);
            return Ok(result);
        }
    }
}
