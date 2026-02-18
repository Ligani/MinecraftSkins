using MinecraftSkins.Domain.Exceptions;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Repositories;
using MinecraftSkins.Services.DTO;
using MinecraftSkins.Services.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Services.Logics
{
    public class SkinService : ISkinService
    {
        private readonly ISkinRepository _skinRepository;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IExchangeRateService _exchangeRateService;
        public SkinService(ISkinRepository skinRepository,
            IPriceCalculator priceCalculator,
            IExchangeRateService exchangeRateService)
        {
            _skinRepository = skinRepository;
            _priceCalculator = priceCalculator;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<Guid> CreateAsync(
            string name, 
            decimal basePriceUsd, 
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessException("Skin name is required.");

            if (basePriceUsd <= 0)
                throw new BusinessException("The skin price must be a positive number.");

            var skin = new Skin(name, basePriceUsd);

            await _skinRepository.AddAsync(skin, cancellationToken);

            return skin.Id;
        }

        public async Task<PagedResponse<SkinResponse>> GetPagedAsync(
            bool availableOnly,
            string? search,
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


            var pagedSkins = await _skinRepository.GetPagedAsync(availableOnly,
                search,
                pageNumber,
                pageSize, 
                cancellationToken);

            var exchangeRate = await _exchangeRateService.GetRateAsync(cancellationToken);

            return new PagedResponse<SkinResponse>
            {
                Items = pagedSkins.Items.Select(s => 
                new SkinResponse(
                    s.Id, 
                    s.Name,
                    _priceCalculator.CalculateBtcPrice(s.BasePriceUsd, exchangeRate),
                    s.IsAvailable,
                    s.CreatedAtUtc,
                    s.UpdatedAtUtc)).ToList(),

                PageNumber = pagedSkins.PageNumber,

                PageSize = pagedSkins.PageSize,

                TotalCount = pagedSkins.TotalCount,
            };
        }

        public async Task<SkinResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var skin = await _skinRepository.GetByIdAsync(id, cancellationToken);

            if (skin is null  || skin.IsDeleted)
                throw new NotFoundException("Skin not found.");

            if (!skin.IsAvailable)
                throw new UnavailableException("The skin is already sold or unavailable.");

            var exchangeRate = await _exchangeRateService.GetRateAsync(cancellationToken);

            return new SkinResponse(
                skin.Id, 
                skin.Name,
                _priceCalculator.CalculateBtcPrice(skin.BasePriceUsd, exchangeRate),
                skin.IsAvailable,
                skin.CreatedAtUtc, skin.UpdatedAtUtc);
        }

        public async Task UpdateAsync(
            Guid id,
            string name,
            decimal basePriceUsd, 
            bool isAvailable,
            CancellationToken cancellationToken)
        {
            var skin = await _skinRepository.GetByIdAsync(id, cancellationToken);


            if (skin is null)
                throw new NotFoundException("Skin not found");
            if (basePriceUsd <= 0)
                throw new BusinessException("The skin price must be a positive number.");

            skin.Update(name, basePriceUsd, isAvailable);

            await _skinRepository.UpdateAsync(skin, cancellationToken);
        }

        public async Task DeleteAsync(
            Guid id, 
            CancellationToken cancellationToken)
        {
            var skin = await _skinRepository.GetByIdAsync(id, cancellationToken);

            if (skin is null)
                throw new NotFoundException("Skin not found");

            skin.Delete();

            await _skinRepository.UpdateAsync(skin, cancellationToken);
        }
    }
}
