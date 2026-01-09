using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimiterDemo.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        [EnableRateLimiting("dataLimiter")]

        public IActionResult GetData()
        {
            return Ok(new
            {
                Allowed = true,
                Message = "Request allowed",
                Timestamp = DateTime.UtcNow
            });
        }
    }

}
