using ProjektAPBD.DTOs.RevenueDTOs;
using ProjektAPBD.Repository;

namespace ProjektAPBD.Services;

public class RevenueService : IRevenueService
{
    private readonly IAgreementsRepository _agreementsRepository;
    public RevenueService(IAgreementsRepository agreementsRepository)
    {
        _agreementsRepository = agreementsRepository;
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
        decimal revenue = await ProductRevenue(productId, estimatedRevenue);

        return new ProductRevenueDTO()
        {
            ProductId = productId,
            Value = revenue
        };
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