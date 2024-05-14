using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class UniversityEntity
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("country")]
        public int country_id { get; set; }
        public CountryEntity country { get; set; }
        public string university_name { get; set; }

        public ISet<University_yearEntity>? university_years { get; set; }
        public ISet<University_ranking_yearEntity>? university_ranking_years { get; set; }
    }
}
