using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models
{
    public class University_ranking_year
    {
        public int university_id { get; set; }
        public int ranking_criteria_id { get; set; }
        public int year { get; set; }
        public int score { get; set; }
    }
}
