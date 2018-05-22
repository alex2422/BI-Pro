namespace RevisionFyn.BI_Pro.Model
{
    public class Employee
    {
        #region Variables
        private static Employee employeeInstance;
        public double Hours { get; set; }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

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
