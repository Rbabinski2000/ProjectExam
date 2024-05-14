using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Filters
{
    public class CountryFilter
    {
        public int? Id {  get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string SortBy {  get; set; }=string.Empty;
        public int SortDirection { get; set; }
    }
}
