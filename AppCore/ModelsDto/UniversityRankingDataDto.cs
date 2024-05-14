using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.ModelsDto
{
    public class UniversityRankingDataDto
    {
        public int universityId { get; set; }
        public string universityName { get; set; }
        public List<scoresDto> scores { get; set; }
    }
}
