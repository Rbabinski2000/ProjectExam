using AppCore.Filters;
using AppCore.Models;
using AutoMapper;
using Infastructure.Services.Interfaces;
using Infrastracture.Context;
using Infrastracture.Entities;
using Infrastracture.Sorting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Services
{
    public class CountryService:ICountryService
    {
        public AppDbContext _context;
        public IMapper _mapper;
        public CountryService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Country>> Get(CountryFilter filter)
        {
            try
            {
                IQueryable<CountryEntity> query = _context.countries.AsQueryable();

                //Filter
                if (filter.Id != null) {
                    query = query.Where(x => x.id == filter.Id);
                }


                //Sort
                if(!string.IsNullOrEmpty(filter.SortBy))
                {
                    query = Sorter.ApplyDynamicSorting(query,filter.SortBy,filter.SortDirection);

                    
                }
                
                
                //page
                query = query.Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize);
                var result =await query.ToListAsync();

                List<Country> mappedCountries=_mapper.Map<List<Country>>(result);

                return mappedCountries;
            }catch (Exception ex) { 
            
                return new List<Country>();
            }
        }
        public int Add(Country country)
        {
            try
            {
                CountryEntity temp = _mapper.Map<CountryEntity>(country);

                _context.countries.AddAsync(temp);
                _context.SaveChangesAsync();
                return country.id;
            }catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> Update(Country country,int country_id)
        {
            
            try
            {
                CountryEntity temp= _mapper.Map<CountryEntity>(country);
                CountryEntity? existingContry = await _context.countries.FindAsync(country_id);
                var props = typeof(CountryEntity).GetProperties();
                foreach(var prop in props)
                {
                    if(prop.PropertyType == typeof(string))
                    {
                        string newS=(string )prop.GetValue(temp);
                        if (!string.IsNullOrEmpty(newS))
                        {
                            prop.SetValue(existingContry, newS);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int countryId)
        {
            try
            {
                CountryEntity? existingC = await _context.countries.FindAsync(countryId);
                if (existingC is null)
                {
                    return false;
                }
                _context.countries.Remove(existingC);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<int> CountCountries()
        {
            return await _context.countries.CountAsync();
        }


    }
}
