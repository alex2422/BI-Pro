using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class Client
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int ClientStartYear { get; set; }
        public int ClientEndYear { get; set; }
        public List<double> Coverages { get; set; }
        public double[] x { get; set; }
        public double[] y { get; set; }
        public string color { get; set; }
        public List<AccountCard> accountCards { get; set; }
        public List<int> years { get; set; }
        public Employee MainEmployee { get; set; }
        public Client()
        {
            Coverages = new List<double>();
            years = new List<int>();
            accountCards = new List<AccountCard>();
            foreach (AccountCard accCard in accountCards)
            {
                if (accCard.ClientID != ClientID)
                {
                    accountCards.Remove(accCard);
                }
            }
            accountCards.Sort((x, y) => x.Year.CompareTo(y.Year));
        }
    }
}
