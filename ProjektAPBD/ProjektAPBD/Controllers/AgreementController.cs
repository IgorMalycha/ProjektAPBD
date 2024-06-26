﻿using Microsoft.AspNetCore.Mvc;
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


    [HttpPost]
    public async Task<IActionResult> AddNewAgreement(AddAgreementDTO addAgreementDto)
    {
        //output dodac
        await _agreementService.MakeNewAgreement(addAgreementDto);
            

        return Created();
    }
}