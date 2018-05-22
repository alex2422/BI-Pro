using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class AccountCard
    {
        public string CaseID { get; set; }
        public int Year { get; set; }
        public Employee MainEmployee { get; set; }
        public List<Employee> otherEmployees { get; set; } = new List<Employee>();
        public int NumberOfTasks { get; set; }
        public int InvoicePrice { get; set; }
        public int Balance { get; set; }
        public int TotalConsumption { get; set; }
        public string ClientName { get; set; }
        public int ClientID { get; set; }
        public double totalHours { get; set; }
    }
}
