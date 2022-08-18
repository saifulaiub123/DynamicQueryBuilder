using Microsoft.AspNetCore.Mvc;

namespace Involys.Poc.Api.Controllers.Common
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
