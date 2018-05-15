using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Database;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Controller
{
    public class OverviewController
    {
        #region Variables
        private static OverviewController controllerInstance;
        ExcelExport excelInstance;
        public StoredProcedure _StoreProcedure { get; set; }

        ObservableCollection<string> companyData = new ObservableCollection<string>();
        ObservableCollection<string> companyData2 = new ObservableCollection<string>();

        public ListBox LeftBox { get; set; }
        public ListBox RightBox { get; set; }
        #endregion

        #region Constructor
        private OverviewController(ListBox leftBox, ListBox rightBox)
        {
            string companyName = "";
            int balance = 0;
            int companyID = 0;
            int employeeID = 0;
            int companyStartYear = 0;

            excelInstance = new ExcelExport(companyName, balance,  companyID,  employeeID,  companyStartYear);

            LeftBox = leftBox;
            RightBox = rightBox;
            rightBox.ItemsSource = companyData2;
            leftBox.ItemsSource = companyData;

            _StoreProcedure = new StoredProcedure();
        }
        #endregion

        #region Public methods
        public static OverviewController GetInstance(ListBox leftBox, ListBox rightBox)
        {
            if (controllerInstance == null)
            {
                controllerInstance = new OverviewController(leftBox, rightBox);
            }
            return controllerInstance;
        }

        public void ButtonTest(ListBox companies)
        {
            companies.ItemsSource = _StoreProcedure.GetCompanies();
        }

        public void ButtonAdd()
        {
            companyData2.Add(Convert.ToString(LeftBox.SelectedItem));
            companyData.Remove(Convert.ToString(LeftBox.SelectedItem));
        }

        public void ButtonRemove()
        {
            companyData.Add(Convert.ToString(RightBox.SelectedItem));
            companyData2.Remove(Convert.ToString(RightBox.SelectedItem));
        }


        public void LoadIntoComoBox(ComboBox yearsBox)
        {
            yearsBox.ItemsSource = _StoreProcedure.GetYear();
        }


        public void ExportData()
        {
            _StoreProcedure.GetCompanies();
        }
        #endregion

        #region Private methods

        #endregion

    }
}
