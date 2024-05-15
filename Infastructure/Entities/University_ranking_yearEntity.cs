using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class University_ranking_yearEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("university")]
        public int university_id { get; set; }
        public UniversityEntity university { get; set; }
        [ForeignKey("ranking_criteria")]
        public int ranking_criteria_id { get; set; }
        public Ranking_criteriaEntity ranking_criteria { get; set; }
        public int year { get; set; }
        public int? score { get; set; }
    }
}
