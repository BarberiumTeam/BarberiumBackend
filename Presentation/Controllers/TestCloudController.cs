using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCloudController : ControllerBase
    {

        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Pong from Cloud Controller");
        }
    }
}
