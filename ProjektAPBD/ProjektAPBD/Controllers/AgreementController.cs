using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.DTOs.AgreementDTOs;
using ProjektAPBD.Services;

namespace ProjektAPBD.Controllers;

[Route("api/agreement")]
[ApiController]
public class AgreementController : ControllerBase
{
    private readonly IAgreementService _agreementService;
    
    public AgreementController(IAgreementService agreementService)
    {
        _agreementService = agreementService;
    }

    // nie dziala na swaggerze
    // [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddNewAgreement(AddAgreementDTO addAgreementDto)
    {

        await _agreementService.MakeNewAgreement(addAgreementDto);
        
        return Created("api/agreement", addAgreementDto);
    }
    
    // nie dziala na swaggerze
    // [Authorize]
    [HttpPut("{agreementId}/payment")]
    public async Task<IActionResult> PaymentForAgreement(int agreementId, [FromQuery]decimal paymentValue)
    {
        var result = await _agreementService.PayForAgreemnt(agreementId ,paymentValue);

        return Ok(result);
    }
}