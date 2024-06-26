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
}