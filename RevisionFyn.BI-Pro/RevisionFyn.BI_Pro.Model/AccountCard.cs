using System.Collections.Generic;

namespace RevisionFyn.BI_Pro.Model
{
    public class AccountCard
    {
        public string CaseID { get; set; }
        public int Year { get; set; }
        public Employee MainEmployee { get; set; }
        public int NumberOfTasks { get; set; }
        public int InvoicePrice { get; set; }
        public int Balance { get; set; }
        public int TotalConsumption { get; set; }
        public string ClientName { get; set; }
        public int ClientID { get; set; }
        public double TotalHours { get; set; }
    }
}
