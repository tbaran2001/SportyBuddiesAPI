using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportyBuddiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetUsers()
        {
            return new JsonResult(
                new List<object>
                {
                    new { Id = 1, Name = "John" },
                    new { Id = 2, Name = "Bob" }
                });
        }
        
        [HttpGet("{id}")]
        public JsonResult GetUser(int id)
        {
            return new JsonResult(new { Id = id, Name = "John" });
        }
    }
}
