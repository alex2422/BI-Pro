using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class CustomStatistics
    {
        public StatisticsType ChoosenStatisticsType { get; set; }
        public List<Company> ChoosenCompanies { get; set; }
    }
}
