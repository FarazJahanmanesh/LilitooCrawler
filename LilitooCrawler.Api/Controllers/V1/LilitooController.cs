using ExternalServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LilitooCrawler.Api.Controllers.V1;
public class LilitooController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetMo()
    {
        LilitooServices a = new LilitooServices();
        await a.GetUncategorizedProduct();
        return Ok();
    }
}
