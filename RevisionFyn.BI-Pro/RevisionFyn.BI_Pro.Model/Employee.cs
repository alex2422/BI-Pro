using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RevisionFyn.BI_Pro.Model
{
    public class Employee
    {
        #region Variables
        private static Employee employeeInstance;
        public double Hours { get; set; }
        public int EmployeeID { get; set; }

        #endregion

        #region Private methods

        #endregion

        #region Public methods
        public static Employee GetInstance()
        {
            if (employeeInstance == null)
            {
                employeeInstance = new Employee();
            }
            return employeeInstance;
        }

        #endregion
    }
}
