using AppCore.Filters;
using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>> Get(CountryFilter filter);
        int Add(Country country);
        Task<bool> Update(Country country, int country_id);
        Task<bool> Delete(int countryId);
        Task<int> CountCountries();
    }
}
