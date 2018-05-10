using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    class AccountCard
    {
        public int AccountCardID { get; set; }
        public int Year { get; set; }
        public int MainEmployee { get; set; }
        public List<int> otherEmployees;
        public int NumberOfTasks { get; set; }
        public int InvoicePrice { get; set; }
        public int Coverage { get; set; }
    }
}
