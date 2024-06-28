using ProjektAPBD.DTOs.RevenueDTOs;

namespace ProjektAPBD.Services;

public interface IRevenueService
{
    Task<CompanyRevenueDTO> GetCompanyRevenue(bool estimatedRevenue);
    Task<ProductRevenueDTO> GetProductRevenue(int productId, bool estimatedRevenue);

}