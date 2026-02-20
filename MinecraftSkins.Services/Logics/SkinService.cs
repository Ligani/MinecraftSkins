using MinecraftSkins.Domain.Exceptions;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Services.DTO;
using MinecraftSkins.Services.Interfaces.IRepositories;
using MinecraftSkins.Services.Interfaces.IServices;

namespace MinecraftSkins.Services.Logics
{
    public class SkinService : ISkinService
    {
        private readonly ISkinRepository _skinRepository;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IExchangeRateService _exchangeRateService;
        public SkinService(ISkinRepository skinRepository, IPriceCalculator priceCalculator, IExchangeRateService exchangeRateService)
        {
            _skinRepository = skinRepository;
            _priceCalculator = priceCalculator;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<Guid> CreateAsync(string name, decimal basePriceUsd, CancellationToken cancellationToken)
        {
            try
            {
                var skin = new Skin(name, basePriceUsd);
                await _skinRepository.AddAsync(skin, cancellationToken);

                return skin.Id;
            }
            catch (DomainException ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<PagedResponse<SkinResponse>> GetPagedAsync(bool availableOnly, string? search, int pageNumber,  int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber < 1)
                throw new BusinessException("Page number must be greater than 0.");

            if (pageSize < 1 || pageSize > 100)
                throw new BusinessException("Page size must be between 1 and 100.");

            var pagedSkins = await _skinRepository.GetPagedAsync(availableOnly,search,pageNumber,pageSize, cancellationToken);

            var exchangeRate = await _exchangeRateService.GetRateAsync(cancellationToken);

            return new PagedResponse<SkinResponse>
            {
                Items = pagedSkins.Items.Select(s => new SkinResponse(s.Id, s.Name,_priceCalculator.CalculateBtcPrice(s.BasePriceUsd, exchangeRate),
                    s.IsAvailable,s.CreatedAtUtc,s.UpdatedAtUtc)).ToList(),
                PageNumber = pagedSkins.PageNumber, PageSize = pagedSkins.PageSize, TotalCount = pagedSkins.TotalCount,
            };
        }

        public async Task<SkinResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var skin = await _skinRepository.GetByIdAsync(id, cancellationToken);

            if (skin is null  || skin.IsDeleted)
                throw new NotFoundException("Skin not found.");

            if (!skin.IsAvailable)
                throw new UnavailableException("The skin is already sold or unavailable.");

            var exchangeRate = await _exchangeRateService.GetRateAsync(cancellationToken);

            return new SkinResponse(skin.Id, skin.Name, _priceCalculator.CalculateBtcPrice(skin.BasePriceUsd, exchangeRate), skin.IsAvailable, 
                skin.CreatedAtUtc, skin.UpdatedAtUtc);
        }

        public async Task UpdateAsync(Guid id, string name, decimal basePriceUsd,  bool isAvailable, CancellationToken cancellationToken)
        {
            var skin = await _skinRepository.GetByIdAsync(id, cancellationToken);

            if (skin is null)
                throw new NotFoundException("Skin not found");

            try
            {
                skin.Update(name, basePriceUsd, isAvailable);
                await _skinRepository.UpdateAsync(skin, cancellationToken);
            }
            catch (DomainException ex)
            {
                throw new BusinessException(ex.Message);
            }

        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var skin = await _skinRepository.GetByIdAsync(id, cancellationToken);

            if (skin is null)
                throw new NotFoundException("Skin not found");

            skin.Delete();

            await _skinRepository.UpdateAsync(skin, cancellationToken);
        }
    }
}
