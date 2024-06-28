using ProjektAPBD.DTOs.RevenueDTOs;
using ProjektAPBD.Repository;

namespace ProjektAPBD.Services;

public class RevenueService : IRevenueService
{
    private readonly IAgreementsRepository _agreementsRepository;
    private readonly ISoftwareRepository _softwareRepository;
    public RevenueService(IAgreementsRepository agreementsRepository, ISoftwareRepository softwareRepository)
    {
        _agreementsRepository = agreementsRepository;
        _softwareRepository = softwareRepository;
    }

    public async Task<CompanyRevenueDTO> GetCompanyRevenue(bool estimatedRevenue)
    {
        decimal revenue = await CompanyRevenue(estimatedRevenue);

        return new CompanyRevenueDTO()
        {
            Value = revenue
        };
    }
    
    public async Task<ProductRevenueDTO> GetProductRevenue(int productId, bool estimatedRevenue)
    {
        await DoesSoftwareExist(productId);
        decimal revenue = await ProductRevenue(productId, estimatedRevenue);

        return new ProductRevenueDTO()
        {
            ProductId = productId,
            Value = revenue
        };
    }

    private async Task DoesSoftwareExist(int productId)
    {
        if (!await _softwareRepository.DoesSoftwareExistById(productId))
        {
            throw new ArgumentException($"Software with id: {productId} does not exist");
        }
    }

    private async Task<decimal> CompanyRevenue(bool estimatedRevenue)
    {
        if (estimatedRevenue)
        {
            return await _agreementsRepository.GetCompanyEstimatedRevenue();
        }

        return await _agreementsRepository.GetCompanyRevenue();
    }
    
    private async Task<decimal> ProductRevenue(int productId, bool estimatedRevenue)
    {
        if (estimatedRevenue)
        {
            return await _agreementsRepository.GetProductEstimatedRevenue(productId);
        }
        return await _agreementsRepository.GetProductRevenue(productId);
    }

    
}