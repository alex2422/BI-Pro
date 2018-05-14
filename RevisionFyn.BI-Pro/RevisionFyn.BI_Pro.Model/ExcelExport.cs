using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows;




namespace RevisionFyn.BI_Pro.Model
{
    public class ExcelExport
    {
        #region Variables
        private static ExcelExport excelInstance;
        //SaveFileDialog saveFileDialog { get; set; }
        StringBuilder csvImport { get; set; }
        List<string> Header { get; set; } = new List<string>();

        public string CompanyName { get; set; }
        public int Balance { get; set; }
        public int CompanyID { get; set; }
        public int EmployeeID { get; set; }
        public int CompanyStartYear { get; set; }

        #endregion

        #region Constructor
        private ExcelExport(string companyName, int balance, int companyID, int employeeID, int companyStartYear)
        {
            CompanyName = companyName;
            Balance = balance;
            CompanyID = companyID;
            EmployeeID = employeeID;
            CompanyStartYear = companyStartYear;
        }
        #endregion

        #region Public methods
        public static ExcelExport GetInstance(string companyName, int balance, int companyID, int employeeID, int companyStartYear)
        {
            if (excelInstance == null)
            {
                excelInstance = new ExcelExport(companyName, balance, companyID, employeeID, companyStartYear);
            }
            return excelInstance;
        }
        public void Export(string CompanyName, int Balance, int ComapnyID, int EmployeeID, int ComapanyStartYear)
        {
            Header.Add(CompanyName); 
            Header.Add(Balance.ToString());
            Header.Add(CompanyID.ToString());
            Header.Add(EmployeeID.ToString());
            Header.Add(CompanyStartYear.ToString());
        }

        #endregion

        #region Private methods
        private void TrialBuilder()
        {
            csvImport = new StringBuilder();

            csvImport.AppendLine(String.Format("{0},{1},{2},{3},{4}", Header[0], Header[1], Header[2], Header[3], Header[4]));

            File.WriteAllText(@"C:\Users\Bruger\Desktop\test.csv", csvImport.ToString());
        }

        #endregion


    }
}
