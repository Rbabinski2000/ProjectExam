using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models
{
 
    public class Country
    {
        public int id { get; set; }
        public string country_name { get; set; }
    }
}
