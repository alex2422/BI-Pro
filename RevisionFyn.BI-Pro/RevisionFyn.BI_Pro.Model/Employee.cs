using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RevisionFyn.BI_Pro.Model
{
    class Employee
    {
        #region Variables
        public double Hours { get; set; }
        public int EmployeeID { get; set; }

        public List<int> employeeID = new List<int>();

        #endregion
    }
}
