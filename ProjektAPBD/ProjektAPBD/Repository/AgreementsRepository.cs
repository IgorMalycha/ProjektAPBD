using Microsoft.EntityFrameworkCore;
using ProjektAPBD.Context;
using ProjektAPBD.Models;

namespace ProjektAPBD.Repository;

public class AgreementsRepository : IAgreementsRepository
{
    private readonly DatabaseContext _databaseContext;

    public AgreementsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddNewAgreement(Agreement newAgreement)
    {
        await _databaseContext.Agreements.AddAsync(newAgreement);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<bool> IsThereAlreadyAgreementOnSoftware(Software software, int clientid, bool isCompanyClient)
    {
        if (isCompanyClient)
        {
            return await _databaseContext.Agreements.Where(e => e.Software.SoftwareId == software.SoftwareId)
                .AnyAsync(e => e.CompanyClientId == clientid);
        }
        return await _databaseContext.Agreements.Where(e => e.Software.SoftwareId == software.SoftwareId)
            .AnyAsync(e => e.IndividualClientId == clientid);
    }

    public async Task<bool> DoesAgreemntExistById(int agreementId)
    {
        return await _databaseContext.Agreements.AnyAsync(e => e.AgreementId == agreementId);
    }

    public async Task<Agreement> GetAgreementById(int agreementId)
    {
        return await _databaseContext.Agreements
            .FirstOrDefaultAsync(e => e.AgreementId == agreementId);
    }

    public async Task CancelAgreement(Agreement agreement)
    {
        _databaseContext.Agreements.Remove(agreement);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task AddPayment(Agreement agreement, decimal paymentValue)
    {
        if (agreement.Payment + paymentValue >= agreement.Price)
        {
            agreement.Payment = agreement.Price;
            
        }
        else
        {
            agreement.Payment += paymentValue;
        }
        await _databaseContext.SaveChangesAsync();
    }

    public async Task SignAgreement(Agreement agreement)
    {
        agreement.Signed = true;
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<decimal> GetCompanyEstimatedRevenue()
    {
        return await _databaseContext.Agreements.SumAsync(e => e.Price);
    }

    public async Task<decimal> GetCompanyRevenue()
    {
        return await _databaseContext.Agreements.Where(e => e.Signed == true)
            .SumAsync(e => e.Price);
    }

    public async Task<decimal> GetProductEstimatedRevenue(int productId)
    {
        return await _databaseContext.Agreements.Where(e => e.SoftwareId == productId)
            .SumAsync(e => e.Price);
    }

    public async Task<decimal> GetProductRevenue(int productId)
    {
        return await _databaseContext.Agreements.Where(e => e.Signed == true)
            .Where(e => e.SoftwareId == productId)
            .SumAsync(e => e.Price);
    }
}