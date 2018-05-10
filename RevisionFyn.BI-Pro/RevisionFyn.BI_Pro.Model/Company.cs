using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class Company
    {
        public string CompanyName { get; set; }
        public int CompanyStartYear { get; set; }
        public int CompanyEndYear { get; set; }
        public List<double> Coverages;
        public double[] x;
        public double[] y;
        public string colour;
        public int CompanyID;
        public List<AccountCard> accountCards;
        public List<int> years;
        public Employee MainEmployee { get; set; }
    }
}
