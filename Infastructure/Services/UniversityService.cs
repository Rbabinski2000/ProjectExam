using AppCore.Filters;
using AppCore.Models;
using AppCore.ModelsDto;
using AutoMapper;
using Infrastracture.Context;
using Infrastracture.Entities;
using Infrastracture.Sorting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infastructure.Services
{
    public class UniversityService
    {
        public AppDbContext _context;
        public IMapper _mapper;
        public UniversityService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UniversityRankingDataDto>> GetByCountry(string countryName)
        {
            try
            {
                if (!string.IsNullOrEmpty(countryName))
                {

                    IQueryable<UniversityEntity> query1 = _context.universities.AsQueryable();
                    query1 = query1
                        .Include(x => x.country)
                        .Where(x => x.country.country_name == countryName);



                    List<UniversityRankingDataDto> result = new List<UniversityRankingDataDto>();
                    var university = await query1.ToListAsync();
                    foreach (var univer in university)
                    {
                        IQueryable<University_ranking_yearEntity> queryTemp = _context.university_rankings.AsQueryable();
                        queryTemp = queryTemp
                            .Include(x => x.university)
                            .Include(x => x.ranking_criteria)
                            .Where(x => x.university_id == univer.id);

                        var restemp = await queryTemp.ToListAsync();
                        List<scoresDto> scores = _mapper.Map<List<scoresDto>>(restemp);
                        for (int i = 0; i < restemp.Count; i++)
                        {
                            scores[i].criteriaName = restemp[i].ranking_criteria.criteria_name;
                        }
                        UniversityRankingDataDto temp = new UniversityRankingDataDto()
                        {
                            universityId = univer.id,
                            universityName = univer.university_name,
                            scores = scores
                        };

                        result.Add(temp);
                    }
                    /*IQueryable<University_ranking_yearEntity> query = _context.university_rankings.AsQueryable();
                    query = query
                        .Include(x => x.university)
                        .Include(x=>x.university.country);

                    //Sort
                    if (!string.IsNullOrEmpty(countryName))
                    {
                        query = query.Where(x => x.university.country.country_name == countryName);


                    }*/
                    //var result = await query.ToListAsync();

                    //List<University> mappedUniversities = _mapper.Map<List<University>>(result);
                    return result;
                    //return mappedUniversities;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {

                return new List<UniversityRankingDataDto>();
            }

        }
        public async Task<List<University>> Get(CountryFilter filter)
        {
            try
            {
                IQueryable<UniversityEntity> query = _context.universities.AsQueryable();

                //Filter
                if (filter.Id != null)
                {
                    query = query.Where(x => x.id == filter.Id);
                }


                //Sort
                if (!string.IsNullOrEmpty(filter.SortBy))
                {
                    query = Sorter.ApplyDynamicSorting(query, filter.SortBy, filter.SortDirection);


                }


                //page
                query = query.Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize);
                var result = await query.ToListAsync();

                List<University> mappedUniversities = _mapper.Map<List<University>>(result);

                return mappedUniversities;
            }
            catch (Exception ex)
            {

                return new List<University>();
            }
        }
        public int Add(University university)
        {
            try
            {
                UniversityEntity temp = _mapper.Map<UniversityEntity>(university);

                _context.universities.AddAsync(temp);
                _context.SaveChangesAsync();
                return university.id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<string> AddUniversityRanking(University_ranking_year universityRanking)
        {
            try
            {
                // Sprawdź, czy wpis dla danego roku i kryterium już istnieje
                var existingScore = await _context.university_rankings
                    .FirstOrDefaultAsync(ury => ury.university_id == universityRanking.university_id 
                    && ury.year == universityRanking.year 
                    && ury.ranking_criteria_id == universityRanking.ranking_criteria_id);

                if (existingScore != null)
                {
                    throw new Exception();
                }
                University_ranking_yearEntity temp = _mapper.Map<University_ranking_yearEntity>(universityRanking);

                await _context.university_rankings.AddAsync(temp);
                await _context.SaveChangesAsync();
                return $"{universityRanking.university_id} {universityRanking.ranking_criteria_id}";
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        public async Task<bool> Update(University university, int university_id)
        {

            try
            {
                UniversityEntity temp = _mapper.Map<UniversityEntity>(university);
                UniversityEntity? existingContry = await _context.universities.FindAsync(university_id);
                var props = typeof(UniversityEntity).GetProperties();
                foreach (var prop in props)
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        string newS = (string)prop.GetValue(temp);
                        if (!string.IsNullOrEmpty(newS))
                        {
                            prop.SetValue(existingContry, newS);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int universityId)
        {
            try
            {
                UniversityEntity? existingC = await _context.universities.FindAsync(universityId);
                if (existingC is null)
                {
                    return false;
                }
                _context.universities.Remove(existingC);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<int> CountUniversity()
        {
            return await _context.universities.CountAsync();
        }


    }
}


