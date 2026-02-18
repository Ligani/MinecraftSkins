using MinecraftSkins.Domain.Exceptions;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Repositories;
using MinecraftSkins.Services.DTO;
using MinecraftSkins.Services.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Services.Logics
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ISkinRepository _skinRepository;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IExchangeRateService _exchangeRateService;

        public PurchaseService(IPurchaseRepository purchaseRepository,
            ISkinRepository skinRepository,
            IPriceCalculator priceCalculator,
            IExchangeRateService exchangeRateService)
        {
            _purchaseRepository = purchaseRepository;
            _skinRepository = skinRepository;
            _priceCalculator = priceCalculator;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<PurchaseResponse> CreateAsync(
            Guid skinId,
            Guid buyerId, 
            CancellationToken cancellationToken)
        {
            //await Task.Delay(3000, cancellationToken);

            var skin = await _skinRepository.GetByIdAsync(skinId, cancellationToken);
            if (skin == null || skin.IsDeleted)
                throw new NotFoundException("Skin not found");

            if (!skin.IsAvailable)
            {
                throw new UnavailableException("Skin is no longer available.");
            }

            var currentRate = await _exchangeRateService.GetRateAsync(cancellationToken);

            var finalPrice = _priceCalculator.CalculateBtcPrice(skin.BasePriceUsd, currentRate);

            var success = await _skinRepository.TryMarkAsSoldAsync(skinId, cancellationToken);

            if (!success)
            {
                throw new UnavailableException("Sorry, this skin was just purchased.");
            }

            var purchase = new Purchase(skinId, finalPrice, currentRate, buyerId);

            await _purchaseRepository.AddAsync(purchase, cancellationToken);

            return new PurchaseResponse(purchase.Id,
                purchase.SkinId,
                purchase.PriceUsdFinal,
                purchase.BtcUsdRate,
                purchase.PurchasedAtUtc);
        }

        public async Task<PagedResponse<PurchaseResponse>> GetPagedAsync(
            Guid? buyerId,
            bool mineOnly,
            Guid? skinId,
            DateTime? from ,
            DateTime? to ,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            if (pageNumber <= 0)
                pageNumber = 1;

            if (pageSize <= 0)
                pageSize = 10;

            if (pageSize > 100)
                pageSize = 100;

            var pagedPurchases = await _purchaseRepository.GetPagedAsync(
                buyerId,
                mineOnly, 
                skinId, 
                from, 
                to,
                pageNumber,
                pageSize,
                cancellationToken);

            return new PagedResponse<PurchaseResponse>
            {
                Items = pagedPurchases.Items.Select(p => new PurchaseResponse(
                    p.Id,
                    p.SkinId,
                    p.PriceUsdFinal,
                    p.BtcUsdRate,
                    p.PurchasedAtUtc))
                .ToList(),

                PageNumber = pagedPurchases.PageNumber,
                PageSize = pagedPurchases.PageSize,
                TotalCount = pagedPurchases.TotalCount,
            };
        }

        public async Task<PurchaseResponse?> GetByIdAsync(
            Guid id, 
            CancellationToken cancellationToken)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id, cancellationToken);
            if (purchase is null)
                throw new NotFoundException("Purchase not found");

            return new PurchaseResponse(purchase.Id,
                purchase.SkinId,
                purchase.PriceUsdFinal,
                purchase.BtcUsdRate,
                purchase.PurchasedAtUtc);
        }

        public async Task DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id, cancellationToken);

            if (purchase is null)
                throw new NotFoundException("Purchase not found");

            purchase.Delete();

            await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
        }
    }
}
