using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Database;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Controller
{
    public class OverviewController
    {
        #region Variables
        private static OverviewController controllerInstance;
        public StoredProcedure _StoreProcedure { get; set; }

        public List<int> yearList = new List<int>();
        ObservableCollection<Client> companyData;
        ObservableCollection<Client> companyData2;
        #endregion

        #region Constructor
        private OverviewController()
        {
            _StoreProcedure = new StoredProcedure();
        }
        #endregion

        #region Public methods
        public List<Employee> getMainEmployee()
        {
            List<Employee> allEmployees = new List<Employee>();
            allEmployees = _StoreProcedure.getEmployee();
            return allEmployees;
        }
        public static OverviewController GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new OverviewController();
            }
            return controllerInstance;
        }

        public void LoadIntoListBox(ListBox leftBox, ListBox rightBox)
        {
            leftBox.ItemsSource = companyData;
            leftBox.DisplayMemberPath = "CompanyName";
            rightBox.ItemsSource = companyData2;
            rightBox.DisplayMemberPath = "CompanyName";
        }

        public void ClearData()
        {
            if (companyData != null)
            {
                companyData.Clear();
            }
            if (companyData2 != null)
            {
                companyData2.Clear();
            }
        }
        public void PopulateData()
        {
            companyData = new ObservableCollection<Client>(_StoreProcedure.GetCompanies());
            companyData2 = new ObservableCollection<Client>();
        }
        public void ButtonAdd(ListBox LeftBox)
        {
            companyData2.Add((Client)LeftBox.SelectedItem);
            companyData.Remove((Client)LeftBox.SelectedItem);
        }

        public void ButtonRemove(ListBox RightBox)
        {
            companyData.Add((Client)RightBox.SelectedItem);
            companyData2.Remove((Client)RightBox.SelectedItem);
        }

        public void ButtonAddAll(ListBox LeftBox)
        {
            int timesToDo = companyData.Count();
            for (int i = 0; i < timesToDo; i++)
            {
                companyData2.Add((Client)companyData[0]);
                companyData.Remove((Client)companyData[0]);
            }
        }

        public void ButtonRemoveAll(ListBox RightBox)
        {
            int timesToDo = companyData2.Count();
            for (int i = 0; i < timesToDo; i++)
            {
                companyData.Add((Client)companyData2[0]);
                companyData2.Remove((Client)companyData2[0]);
            }
        }


        public void LoadIntoComoBox(ComboBox yearsBox)
        {
            foreach (var accCard in _StoreProcedure.GetYear())
            {
                if (!yearList.Contains(accCard.Year))
                {
                    yearList.Add(accCard.Year);
                }
            }
            yearList.Sort();
            yearsBox.ItemsSource = yearList;
            yearsBox.SelectedItem = yearList[0];
        }


        public void ExportData(ListBox rightBox, ComboBox StartYear)
        {
            if (rightBox.Items.Count != 0)
            {
                ExcelExport export = new ExcelExport();
                string path = export.GetExportPath(rightBox);
                if (path != null && path != "")
                {
                    export.Export(rightBox, StartYear, path, _StoreProcedure.getEmployee());
                }
                else
                {
                    MessageBox.Show("Eksportering annulleret");
                }
            }
            else
            {
                MessageBox.Show("Vælg venligst firmaer");
            }
        }
        #endregion

        #region Private methods
        #endregion

    }
}
