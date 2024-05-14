using AppCore.Filters;
using AppCore.Models;
using Infastructure.Services;
using Infastructure.Services.Interfaces;
using Infrastracture.Context;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers.Intefaces;

namespace ProjectExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        public ICountryService _service;
        

        

        public CountryController(ICountryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries([FromQuery] CountryFilter filter)
        {
            List<Country> result= await _service.Get(filter);
            if (result.Count <= 0) {
                return NotFound();
            }
            return Ok(result);
            
        }
        [HttpPost]
        public async Task<ActionResult<int>> AddCountry([FromBody] string CountryName)
        {
            Country temp=new Country()
            {
                id=await _service.CountCountries()+1,
                country_name=CountryName
            };
            int createUserId = _service.Add(temp);
            if (createUserId == 0)
            {
                return BadRequest("Unable to create country.");
            }
            return CreatedAtAction(nameof(AddCountry), new { id = createUserId });

        }
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] Country updateCountry, [FromQuery] int countryId)
        {
            bool updateResult = await _service.Update(updateCountry, countryId);
            if (!updateResult)
            {
                return BadRequest();
            }
            return Ok(updateResult);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromQuery] int countryId)
        {
            bool deleteResult = await _service.Delete(countryId);
            if (!deleteResult)
            {
                return BadRequest();
            }
            return Ok(deleteResult);
        }
    }
}
