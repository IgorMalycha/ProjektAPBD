using ProjektAPBD.Models;

namespace ProjektAPBD.Repository;

public interface IAgreementsRepository
{
    Task AddNewAgreement(Agreement newAgreement);
    Task<bool> IsThereAlreadyAgreementOnSoftware(Software software, int clientid, bool isCompanyClient);
    Task<bool> DoesAgreemntExistById(int agreementId);
    Task<Agreement> GetAgreementById(int agreementId);

    Task CancelAgreement(Agreement agreement);
    Task AddPayment(Agreement agreement, decimal paymentValue);

    Task SignAgreement(Agreement agreement);
    Task<decimal> GetCompanyEstimatedRevenue();
    Task<decimal> GetCompanyRevenue();
    Task<decimal> GetProductEstimatedRevenue(int productId);
    Task<decimal> GetProductRevenue(int productId);
}