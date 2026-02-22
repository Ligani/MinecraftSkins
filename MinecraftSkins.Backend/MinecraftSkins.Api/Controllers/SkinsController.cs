using Microsoft.AspNetCore.Mvc;
using MinecraftSkins.Services.DTO;
using MinecraftSkins.Services.Interfaces.IServices;

namespace MinecraftSkins.Controllers
{
    [Route("api/skins")]
    [ApiController]
    public class SkinsController : ControllerBase
    {
        private readonly ISkinService _skinService;

        public SkinsController(ISkinService skinService)
        {
            _skinService = skinService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var skin = await _skinService.GetByIdAsync(id, cancellationToken);
            return Ok(skin);
        } 

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SkinRequest request, CancellationToken cancellationToken)
        {
            await _skinService.UpdateAsync(id, request.Name, request.BasePriceUsd, request.IsAvailable, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _skinService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SkinRequest request, CancellationToken cancellationToken)
        {
            var skinId = await _skinService.CreateAsync(request.Name, request.BasePriceUsd, cancellationToken);
            
            return Ok(skinId);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] bool availableOnly = false, [FromQuery] string? search = null, [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var result = await _skinService.GetPagedAsync(availableOnly, search, pageNumber,pageSize,cancellationToken);

            return Ok(result);
        }
    }
}
