using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SportyBuddiesAPI.Models;

namespace SportyBuddiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetSports()
        {
            return Ok(SportyBuddiesDataStore.Current.Sports);
        }

        [HttpGet("{id}")]
        public ActionResult GetSport(int id)
        {
            var sport = SportyBuddiesDataStore.Current.Sports.FirstOrDefault(s => s.Id == id);
            if (sport == null)
            {
                return NotFound();
            }

            return Ok(sport);
        }

        [HttpPost]
        public ActionResult CreateSport(CreateSportDto sport)
        {
            var maxSportId = SportyBuddiesDataStore.Current.Sports.Max(s => s.Id);
            var newSport = new SportDto
            {
                Id = maxSportId + 1,
                Name = sport.Name,
                Description = sport.Description
            };
            SportyBuddiesDataStore.Current.Sports.Add(newSport);
            return CreatedAtRoute("GetSport", new { id = newSport.Id }, newSport);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSport(int id, UpdateSportDto sport)
        {
            var sportFromStore = SportyBuddiesDataStore.Current.Sports.FirstOrDefault(s => s.Id == id);
            if (sportFromStore == null)
            {
                return NotFound();
            }

            sportFromStore.Name = sport.Name;
            sportFromStore.Description = sport.Description;
            return NoContent();
        }
        
        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdateSport(int id, JsonPatchDocument<UpdateSportDto> patchDocument)
        {
            var sportFromStore = SportyBuddiesDataStore.Current.Sports.FirstOrDefault(s => s.Id == id);
            if (sportFromStore == null)
            {
                return NotFound();
            }

            var sportToPatch = new UpdateSportDto
            {
                Name = sportFromStore.Name,
                Description = sportFromStore.Description
            };
            patchDocument.ApplyTo(sportToPatch, ModelState);
            if (!TryValidateModel(sportToPatch))
            {
                return ValidationProblem(ModelState);
            }

            sportFromStore.Name = sportToPatch.Name;
            sportFromStore.Description = sportToPatch.Description;
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteSport(int id)
        {
            var sportFromStore = SportyBuddiesDataStore.Current.Sports.FirstOrDefault(s => s.Id == id);
            if (sportFromStore == null)
            {
                return NotFound();
            }

            SportyBuddiesDataStore.Current.Sports.Remove(sportFromStore);
            return NoContent();
        }
    }
}