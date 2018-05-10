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
        private static Employee employeeInstance;
        public double Hours { get; set; }
        public int EmployeeID { get; set; }

        #endregion

        #region Private methods
        private Employee(double hours, int employeeID)
        {
            Hours = hours;
            EmployeeID = employeeID;
        }

        #endregion

        #region Public methods
        public static Employee GetInstance(double hours, int employeeID)
        {
            if (employeeInstance == null)
            {
                employeeInstance = new Employee(hours, employeeID);
            }
            return employeeInstance;
        }

        #endregion
    }
}
