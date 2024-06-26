using ProjektAPBD.Models;

namespace ProjektAPBD.Repository;

public interface IAgreementsRepository
{
    Task AddNewAgreement(Agreement newAgreement);
    Task<bool> IsThereAlreadyAgreementOnSoftware(Software software, int clientid, bool isCompanyClient);
}