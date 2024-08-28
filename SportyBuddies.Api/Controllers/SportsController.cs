using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Services;
using SportyBuddies.Contracts.Sports;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly ISportsService _sportsService;

        public SportsController(ISportsService sportsService)
        {
            _sportsService = sportsService;
        }

        [HttpPost]
        public IActionResult CreateSport(CreateSportRequest request)
        {
            var sportId = _sportsService.CreateSport(request.SportType.ToString(), request.Name, request.Description,
                request.AdminId);

            var response = new SportResponse(sportId, request.SportType, request.Name, request.Description);

            return Ok(response);
        }
    }
}