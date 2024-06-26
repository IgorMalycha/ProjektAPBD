using ProjektAPBD.Repository;

namespace ProjektAPBD.Services;

public class PaymentService : IPaymentService
{
    private readonly IAgreementsRepository _agreementsRepository;
    private readonly IDiscountsRepository _discountsRepository;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IClientsRepository _clientsRepository;

    public PaymentService(IAgreementsRepository agreementsRepository, IDiscountsRepository discountsRepository, ISoftwareRepository softwareRepository, IClientsRepository clientsRepository)
    {
        _agreementsRepository = agreementsRepository;
        _discountsRepository = discountsRepository;
        _softwareRepository = softwareRepository;
        _clientsRepository = clientsRepository;
    }
    
    
    
    
}