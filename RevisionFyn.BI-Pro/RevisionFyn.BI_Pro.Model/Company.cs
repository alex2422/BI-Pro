using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int CompanyStartYear { get; set; }
        public int CompanyEndYear { get; set; }
        public List<double> Coverages = new List<double>();
        public double[] x;
        public double[] y;
        public string colour;
        public List<AccountCard> accountCards = new List<AccountCard>();
        public List<int> years = new List<int>();
        public Employee MainEmployee { get; set; }
    }
}
