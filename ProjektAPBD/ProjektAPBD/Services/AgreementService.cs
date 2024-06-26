using ProjektAPBD.DTOs.AgreementDTOs;
using ProjektAPBD.Models;
using ProjektAPBD.Repository;

namespace ProjektAPBD.Services;

public class AgreementService : IAgreementService
{
    private readonly IAgreementsRepository _agreementsRepository;
    private readonly IDiscountsRepository _discountsRepository;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IClientsRepository _clientsRepository;

    public AgreementService(IAgreementsRepository agreementsRepository, IDiscountsRepository discountsRepository,
        ISoftwareRepository softwareRepository, IClientsRepository clientsRepository)
    {
        _agreementsRepository = agreementsRepository;
        _discountsRepository = discountsRepository;
        _softwareRepository = softwareRepository;
        _clientsRepository = clientsRepository;
    }

    public async Task MakeNewAgreement(AddAgreementDTO addAgreementDto)
    { 
        //metoda obliczająca 
        //doesSoftwareExist
        //doesClientExist ... add 
        //check if its software to pay in one trancasction not subsritpion
        
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
                Singed = false,
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
                Singed = false,
                IsPaid = false,
                Price = priceDiscountsActualizationYearsIfReturningClient,
                SoftwareVersion = software.Version,
                SoftwareId = software.SoftwareId,
                IndividualClientId = addAgreementDto.Clientid
            };
        }

        await _agreementsRepository.AddNewAgreement(newAgreement);
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