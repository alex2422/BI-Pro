using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class Client
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int CompanyStartYear { get; set; }
        public int CompanyEndYear { get; set; }
        public List<double> Coverages { get; set; } = new List<double>();
        public double[] x { get; set; }
        public double[] y { get; set; }
        public string colour { get; set; }
        public List<AccountCard> accountCards { get; set; } = new List<AccountCard>();
        public List<int> years { get; set; } = new List<int>();
        public Employee MainEmployee { get; set; }
        public Client()
        {
            foreach (AccountCard accCard in accountCards)
            {
                if (accCard.CompanyID != CompanyID)
                {
                    accountCards.Remove(accCard);
                }
            }
            accountCards.Sort((x, y) => x.Year.CompareTo(y.Year));
        }
    }
}
