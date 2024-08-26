using Microsoft.AspNetCore.Mvc;

namespace SportyBuddies.Api.Controllers
{
    [Route("[controller]")]
    public class SportsController : ApiController
    {
        [HttpGet]
        public IActionResult ListSports()
        {
            return Ok(Array.Empty<string>());
        }
    }
}