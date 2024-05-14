using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class Ranking_criteriaEntity
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("ranking_system")]
        public int ranking_system_id { get; set; }
        public Ranking_systemEntity ranking_system { get; set; }
        public string criteria_name { get; set; }
        public ISet<University_ranking_yearEntity>? University_Ranking_Years { get; set; }
    }
}
