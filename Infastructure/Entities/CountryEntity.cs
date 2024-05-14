using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class CountryEntity
    {
        [Key]
        public int id { get; set; }
        public string country_name { get; set; }
        public ISet<UniversityEntity>? university { get; set; }
        
    }
}
