using AppCore.Filters;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Intefaces
{
    public interface ICountryController
    {
        Task<ActionResult<IEnumerable<Country>>> GetCountries([FromQuery] CountryFilter filter);
        Task<ActionResult<int>> AddCountry([FromBody] string CountryName);
        Task<ActionResult<bool>> Put([FromBody] Country updateCountry, [FromQuery] int countryId);
        Task<ActionResult<bool>> Delete([FromQuery] int countryId);

    }
}
