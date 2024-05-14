using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class Ranking_systemEntity
    {
        [Key]
        public int id { get; set; }
        public string system_name { get; set; }
        public ISet<Ranking_criteriaEntity>? ranking_Criterias { get; set; }

    }
}
