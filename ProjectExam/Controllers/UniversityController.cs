using AppCore.Filters;
using AppCore.Models;
using AppCore.ModelsDto;
using Infastructure.Services;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController:ControllerBase
    {
        public UniversityService _service;




        public UniversityController(UniversityService service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("/api/universities")]
        public async Task<ActionResult<IEnumerable<University>>> GetUniversitiesByCountry([FromQuery] string country, [FromQuery] CountryFilter filter)
        {
            List<UniversityRankingDataDto> result = await _service.GetByCountry(filter,country);
            if (result.Count <= 0)
            {
                return NotFound();
            }
            return Ok(result);

        }
        [HttpPost]
        [Route("/api/universities/{id}/scores")]
        public ActionResult<int> AddUniversityRanking(int id,[FromBody] UniversityRankingDto universityRanking)
        {
            if (universityRanking.score < 0 || universityRanking.score > 100)
            {
                return BadRequest("Unable to add ranking. Score is invalid");
            }
            
            University_ranking_year temp = new University_ranking_year()
            {
                university_id=id,
                ranking_criteria_id=universityRanking.rankingCriteriaId,
                score=universityRanking.score,
                year=universityRanking.year,
            };
            
            string createUserId = _service.AddUniversityRanking(temp).Result;
            if (createUserId == "0")
            {
                return BadRequest("Unable to add ranking.");
            }
            return CreatedAtAction(nameof(AddUniversityRanking), new { id = createUserId });

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<University>>> GetUniversities([FromQuery] CountryFilter filter)
        {
            List<University> result = await _service.Get(filter);
            if (result.Count <= 0)
            {
                return NotFound();
            }
            return Ok(result);

        }
        [HttpPost]
        public async Task<ActionResult<int>> AddUniversity([FromBody] University university)
        {
            University temp = new University()
            {
                id = await _service.CountUniversity() + 1,
                country_id =university.country_id,
                university_name=university.university_name
            };
            int createUserId = _service.Add(temp);
            if (createUserId == 0)
            {
                return BadRequest("Unable to create country.");
            }
            return CreatedAtAction(nameof(AddUniversity), new { id = createUserId });

        }
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] University updateUniversity, [FromQuery] int universityId)
        {
            bool updateResult = await _service.Update(updateUniversity, universityId);
            if (!updateResult)
            {
                return BadRequest();
            }
            return Ok(updateResult);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromQuery] int universityId)
        {
            bool deleteResult = await _service.Delete(universityId);
            if (!deleteResult)
            {
                return BadRequest();
            }
            return Ok(deleteResult);
        }
    }
}

