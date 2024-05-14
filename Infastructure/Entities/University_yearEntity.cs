using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class University_yearEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("university")]
        public int university_id { get; set; }
        public UniversityEntity university { get; set; }
        public int year { get; set; } 
        public int num_students { get; set; }
        public int student_staff_ratio { get; set; }
        public int pct_international_students { get; set; }
        public int pct_female_students { get; set; }

    }
}
