using Microsoft.EntityFrameworkCore;
using ProjektAPBD.Context;
using ProjektAPBD.Models;

namespace ProjektAPBD.Repository;

public class SoftwaresRepository : ISoftwareRepository
{
    private readonly DatabaseContext _databaseContext;

    public SoftwaresRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }


    public async Task<List<Discount>> GetAvailableDiscountsBySoftwareId(int softwareId)
    {
        return await _databaseContext.SoftwareDiscounts
            .Where(e => e.SoftwareId == softwareId)
            .Where(e => e.Discount.DateFrom <= DateTime.Now && e.Discount.DateTo >= DateTime.Now)
            .Select(e => e.Discount).ToListAsync();
    }

    public async Task<Software> GetSoftwarePrice(int softwareId)
    {
        return await _databaseContext.Softwares.FirstOrDefaultAsync(e => e.SoftwareId == softwareId);
    }
}