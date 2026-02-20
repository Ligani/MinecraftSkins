using Microsoft.AspNetCore.Mvc;
using MinecraftSkins.Extensions;
using MinecraftSkins.Services.DTO;
using MinecraftSkins.Services.Interfaces.IServices;
using MinecraftSkins.Services.Logics;

namespace MinecraftSkins.Controllers
{
    [Route("api/purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<IActionResult> Buy([FromBody] PurchaseRequest purchaseRequest, CancellationToken cancellationToken)
        {
            var buyerId = HttpContext.GetBuyerId();
            var purchase = await _purchaseService.CreateAsync(purchaseRequest.SkinId, buyerId, cancellationToken);

            return Ok(purchase);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<PurchaseResponse>>> GetPaged([FromQuery] bool mineOnly = false,[FromQuery] Guid? skinId = null,[FromQuery] DateTime? from = null, 
            [FromQuery] DateTime? to = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var buyerId = HttpContext.GetBuyerId();
            var result = await _purchaseService.GetPagedAsync(buyerId,mineOnly, skinId, from, to,pageNumber,pageSize,cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var purchase = await _purchaseService.GetByIdAsync(id, cancellationToken);

            return Ok(purchase);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _purchaseService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        //[HttpPost("test-race")]
        //public async Task<IActionResult> TestRace(Guid skinId, Guid buyerId)
        //{
        //    var scopeFactory = HttpContext.RequestServices.GetRequiredService<IServiceScopeFactory>();

        //    Func<Task> purchaseAction = async () =>
        //    {
        //        using var scope = scopeFactory.CreateScope();
        //        var service = scope.ServiceProvider.GetRequiredService<IPurchaseService>();
        //        await service.CreateAsync(skinId, buyerId, default);
        //    };

        //    var task1 = purchaseAction();
        //    var task2 = purchaseAction();

        //    try
        //    {
        //        await Task.WhenAll(task1, task2);
        //        return Ok("Оба купили? (Это было бы странно)");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}
    }
}
