using ProjektAPBD.DTOs.AgreementDTOs;
using ProjektAPBD.Models;
using ProjektAPBD.Repository;

namespace ProjektAPBD.Services;

public class AgreementService : IAgreementService
{
    private readonly IAgreementsRepository _agreementsRepository;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IClientsRepository _clientsRepository;

    public AgreementService(IAgreementsRepository agreementsRepository, ISoftwareRepository softwareRepository, IClientsRepository clientsRepository)
    {
        _agreementsRepository = agreementsRepository;
        _softwareRepository = softwareRepository;
        _clientsRepository = clientsRepository;
    }

    public async Task MakeNewAgreement(AddAgreementDTO addAgreementDto)
    { 
        
        await DoesSoftwareExist(addAgreementDto.SoftwareId);
        
        await DoesClientExist(addAgreementDto.IsCompanyClient, addAgreementDto.Clientid);
        
        List<Discount> availableDiscounts = await _softwareRepository.GetAvailableDiscountsBySoftwareId(addAgreementDto.SoftwareId);
        
        Software software = await _softwareRepository.GetSoftwarePrice(addAgreementDto.SoftwareId);
        
        decimal priceAfterDiscount = await GetPriceAfterDiscount(software.Price, availableDiscounts);
        
        decimal priceDiscountsActualizationYears = await AddActulizationYearsPrice(priceAfterDiscount, addAgreementDto.actualizationYears);
        
        decimal priceDiscountsActualizationYearsIfReturningClient = await AddDiscountIfReturningClient(addAgreementDto.Clientid, addAgreementDto.IsCompanyClient, priceDiscountsActualizationYears);

        if (await _agreementsRepository.IsThereAlreadyAgreementOnSoftware(software, addAgreementDto.Clientid, addAgreementDto.IsCompanyClient))
        {
            throw new InvalidOperationException("Agreement on this product is still active");
        }

        Agreement newAgreement = new Agreement();
        if (addAgreementDto.IsCompanyClient)
        {
            newAgreement = new Agreement()
            {
                BeginDate = addAgreementDto.BeginDate,
                EndDate = addAgreementDto.EndDate,
                ActualizationYears = addAgreementDto.actualizationYears,
                Signed = false,
                IsPaid = false,
                Price = priceDiscountsActualizationYearsIfReturningClient,
                SoftwareVersion = software.Version,
                SoftwareId = software.SoftwareId,
                CompanyClientId = addAgreementDto.Clientid
            };
        }
        else
        {
            newAgreement = new Agreement()
            {
                BeginDate = addAgreementDto.BeginDate,
                EndDate = addAgreementDto.EndDate,
                ActualizationYears = addAgreementDto.actualizationYears,
                Signed = false,
                IsPaid = false,
                Price = priceDiscountsActualizationYearsIfReturningClient,
                SoftwareVersion = software.Version,
                SoftwareId = software.SoftwareId,
                IndividualClientId = addAgreementDto.Clientid
            };
        }

        await _agreementsRepository.AddNewAgreement(newAgreement);
    }

    private async Task DoesClientExist(bool isCompanyClient, int clientid)
    {
        if (isCompanyClient)
        {
            if (!await _clientsRepository.DoesCompanyClientExist(clientid))
            {
                throw new ArgumentException($"Company client with id: {clientid} does not exist");
            }
        }
        else
        {
            if (!await _clientsRepository.DoesIndividualClientExist(clientid))
            {
                throw new ArgumentException($"Individual client with id: {clientid} does not exist");
            }
        }
    }

    private async Task DoesSoftwareExist(int softwareId)
    {
        if (!await _softwareRepository.DoesSoftwareExistById(softwareId))
        {
            throw new ArgumentException($"Software with id: {softwareId} does not exist");
        }
    }

    public async Task PayForAgreemnt(int agreementId, decimal paymentValue)
    {
        await DoesAgreementExist(agreementId);
        
        Agreement agreement = await _agreementsRepository.GetAgreementById(agreementId);
        
        await IsSigned(agreement.Signed);

        await IsPaymentInTime(agreement);

        await _agreementsRepository.AddPayment(agreement, paymentValue);

        await IsWholePricePaid(agreement);
    }

    private async Task IsWholePricePaid(Agreement agreement)
    {
        if (agreement.Payment == agreement.Price)
        {
            await _agreementsRepository.SignAgreement(agreement);
        }
    }

    private async Task IsPaymentInTime(Agreement agreement)
    {
        if (agreement.EndDate < DateTime.Today)
        {
            await _agreementsRepository.CancelAgreement(agreement);
            throw new InvalidOperationException($"Payment is after ending date of agreement : {agreement.EndDate}");
        }
    }

    private async Task IsSigned(bool signed)
    {
        if (signed)
        {
            throw new ArgumentException("Agreement is already fully paid");
        }
    }

    private async Task DoesAgreementExist(int agreementId)
    {
        if (!await _agreementsRepository.DoesAgreemntExistById(agreementId))
        {
            throw new ArgumentException($"Agreement with Id: {agreementId} does not exist");
        }
    }

    private async Task<decimal> AddDiscountIfReturningClient(int clientid, bool isCompanyClient, decimal price)
    {
        if (await _clientsRepository.IsReturningClient(clientid, isCompanyClient))
        {
            return (price * (decimal)0.05);
        }

        return price;
    }

    private async Task<decimal> AddActulizationYearsPrice(decimal price, int actualizationYears)
    {
        return (price + actualizationYears * 1000);
    }

    private async Task<decimal> GetPriceAfterDiscount(decimal softwarePrice, List<Discount> availableDiscounts)
    {
        decimal newPrice = softwarePrice;
        int maxDiscount = availableDiscounts.Max(e => e.Value);

        return (newPrice * (maxDiscount / 100));
    }
}