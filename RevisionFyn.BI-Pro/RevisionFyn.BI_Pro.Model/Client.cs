using System.Collections.Generic;

namespace RevisionFyn.BI_Pro.Model
{
    public class Client
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int ClientStartYear { get; set; }
        public int ClientEndYear { get; set; }
        public List<double> Coverages = new List<double>();
        public double[] x;
        public double[] y;
        public string colour;
        public List<AccountCard> accountCards = new List<AccountCard>();
        public List<int> years = new List<int>();
        public Employee MainEmployee { get; set; }
        public Client()
        {
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
