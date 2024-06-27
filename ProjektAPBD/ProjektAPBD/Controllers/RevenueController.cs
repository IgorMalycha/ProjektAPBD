using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ProjektAPBD.Services;

namespace ProjektAPBD.Controllers;

[Route("api/revenue")]
[ApiController]
public class RevenueController : ControllerBase
{
    
    private readonly IRevenueService _agreementService;
    
    public RevenueController(IRevenueService agreementService)
    {
        _agreementService = agreementService;
    }

    [HttpGet("company")]
    public async Task<IActionResult> GetCompanyRevenue([FromQuery] bool estimatedRevenue)
    {
        var result = await _agreementService.GetCompanyRevenue(estimatedRevenue);
        
        return Ok(result);
    }
    
    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetProductRevenue(int productId, [FromQuery] bool estimatedRevenue)
    {
        var result = await _agreementService.GetProductRevenue(productId, estimatedRevenue);
        
        return Ok(result);
    }
    
}