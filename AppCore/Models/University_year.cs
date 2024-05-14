using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models
{
    public class University_year
    {
        public int university_id { get; set; }
        public int year { get; set; } 
        public int num_students { get; set; }
        public int student_staff_ratio { get; set; }
        public int pct_international_students { get; set; }
        public int pct_female_students { get; set; }

    }
}
