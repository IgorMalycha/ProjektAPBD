using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.DTOs;
using ProjektAPBD.Services;

namespace ProjektAPBD.Controllers;

[Route("api/clients")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    // nie dziala na swaggerze
    // [Authorize]
    [HttpPost("individualClient")]
    public async Task<IActionResult> AddIndividualClient([FromBody] AddIndividualClientDTO addIndividualClientDto)
    {
        await _clientService.AddIdividualClient(addIndividualClientDto);
        
        return Created();
    }
    
    // nie dziala na swaggerze
    // [Authorize(Roles = "admin")]
    [HttpDelete("individualClient/{individualClientid:int}")]
    public async Task<IActionResult> RemoveIndividualClient(int individualClientid)
    {
        await _clientService.RemoveIdividualClient(individualClientid);

        return NoContent();
    }
    
    // nie dziala na swaggerze
    // [Authorize(Roles = "admin")]
    [HttpPut("individualClient/{individualClientid:int}")]
    public async Task<IActionResult> UpdateIndividualClient(int individualClientid, [FromBody] UpdateIndividualClientDTO updateIndividualClientDto)
    {
        await _clientService.UpdateIdividualClient(individualClientid, updateIndividualClientDto);

        return NoContent();
    }
    
    // nie dziala na swaggerze
    // [Authorize]
    [HttpPost("companyClient")]
    public async Task<IActionResult> AddCompanyClient([FromBody] AddCompanyClientDTO addCompanyClientDto)
    {
        await _clientService.AddCompanyClient(addCompanyClientDto);
        
        return Created();
    }
    
    // nie dziala na swaggerze
    // [Authorize(Roles = "admin")]
    [HttpPut("companyClient/{companyClientid:int}")]
    public async Task<IActionResult> UpdateCompanyClient(int companyClientid, [FromBody] UpdateCompanyClientDTO updateCompanyClientDto)
    {
        await _clientService.UpdateCompanyClient(companyClientid, updateCompanyClientDto);

        return NoContent();
    }
    
    
}