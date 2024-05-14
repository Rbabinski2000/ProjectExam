using AppCore.Models;
using AppCore.ModelsDto;
using AutoMapper;
using Infrastracture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Mapper
{
    public class Mapper:Profile
    {
        public Mapper() {
            CreateMap<CountryEntity, Country>()
                .AfterMap((src, dest) =>
                {
                    dest.id = src.id;
                    dest.country_name = src.country_name;
                    
                });
            CreateMap<Country, CountryEntity>()
                .AfterMap((src, dest) =>
                {
                    dest.id = src.id;
                    dest.country_name = src.country_name;
                });
            CreateMap<UniversityEntity, University>()
                .AfterMap((src, dest) =>
                {
                    dest.id=src.id;
                    dest.country_id = src.country_id;
                    dest.university_name = src.university_name;
                });
            CreateMap<University, UniversityEntity>()
                .AfterMap((src, dest) =>
                {
                    dest.id = src.id;
                    dest.country_id = src.country_id;
                    dest.university_name = src.university_name;
                });
            CreateMap<University_ranking_year, University_ranking_yearEntity>()
                .AfterMap((src, dest) =>
                {
                    dest.university_id = src.university_id;
                    dest.ranking_criteria_id = src.ranking_criteria_id;
                    dest.score = src.score;
                    dest.year = src.year;
                });
            CreateMap<University_ranking_yearEntity, scoresDto>()
                .AfterMap((src, dest) =>
                {
                    dest.score += src.score;
                    dest.year += src.year;
                    
                });
        }
    }
}
