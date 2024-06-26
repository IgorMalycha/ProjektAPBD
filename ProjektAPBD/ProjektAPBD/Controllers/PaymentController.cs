using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.Services;

namespace ProjektAPBD.Controllers;

[Route("api/payment")]
[ApiController]
public class PaymentController
{
    private readonly IPaymentService _paymentService;
    
    public PaymentController(IPaymentService agreementService)
    {
        _paymentService = agreementService;
    }
        
        
}