using System.Collections.Generic;

namespace RevisionFyn.BI_Pro.Model
{
    public class Client
    {
        #region Variables / Properties
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int ClientStartYear { get; set; }
        public int ClientEndYear { get; set; }
        public List<double> Coverages { get; set; }
        public double[] CoordinateX { get; set; }
        public double[] CoordinateY { get; set; }
        public string Color { get; set; }
        public List<AccountCard> AccountCards { get; set; }
        public List<int> Years { get; set; }
        public Employee MainEmployee { get; set; }
        #endregion

        #region Constructor
        public Client()
        {
            Coverages = new List<double>();
            Years = new List<int>();
            AccountCards = new List<AccountCard>();

            foreach (AccountCard accCard in AccountCards)
            {
                if (accCard.ClientID != ClientID)
                {
                    AccountCards.Remove(accCard);
                }
            }

            AccountCards.Sort((x, y) => x.Year.CompareTo(y.Year));
        }
        #endregion
    }
}
