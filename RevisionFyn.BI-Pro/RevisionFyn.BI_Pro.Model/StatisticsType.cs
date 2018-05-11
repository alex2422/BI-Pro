using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class StatisticsType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ExternalSource { get; set; }
        public bool IsActive { get; set; }
    }
}
