using Microsoft.AspNetCore.Mvc;

namespace YKSTAYFA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API çalışıyor 🚀");
        }
    }
}
