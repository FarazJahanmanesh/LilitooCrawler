using Domain.Interfaces.Services;
using ExternalServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace LilitooCrawler.Api.Controllers.V1;
public class LilitooController : BaseController
{
    private readonly ILilitooReadServices _readServices;
    public LilitooController(ILilitooReadServices readServices)
    {
        _readServices = readServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetMo()
    {
        await _readServices.GetUncategorizedProduct();
        return Ok();
    }
}
