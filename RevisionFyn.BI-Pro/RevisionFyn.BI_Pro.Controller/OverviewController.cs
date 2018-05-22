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
        public StoredProcedure _StoredProcedure { get; set; }

        public List<int> yearList = new List<int>();
        public ListBox LeftBox { get; set; }
        public ListBox RightBox { get; set; }
        public ComboBox StartYear { get; set; }
        ObservableCollection<Company> clientData;
        ObservableCollection<Company> clientData2;
        #endregion

        #region Constructor
        private OverviewController(ListBox leftBox, ListBox rightBox, ComboBox startYear)
        {
            StartYear = startYear;
            LeftBox = leftBox;
            RightBox = rightBox;
            clientData2 = new ObservableCollection<Company>();


            _StoredProcedure = new StoredProcedure();
            rightBox.ItemsSource = clientData2;
            leftBox.ItemsSource = clientData;
            rightBox.DisplayMemberPath = "CompanyName";
            leftBox.DisplayMemberPath = "CompanyName";
        }
        #endregion

        #region Public methods
        public List<Employee> getMainEmployee()
        {
            List<Employee> allEmployees = new List<Employee>();
            allEmployees = _StoredProcedure.getEmployee();
            return allEmployees;
        }
        public static OverviewController GetInstance(ListBox leftBox, ListBox rightBox, ComboBox startYear)
        {
            if (controllerInstance == null)
            {
                controllerInstance = new OverviewController(leftBox, rightBox, startYear);
            }
            return controllerInstance;
        }

        public void LoadIntoListBox(ListBox companies)
        {
            companies.ItemsSource = clientData;
            companies.DisplayMemberPath = "CompanyName";
        }

        public void ClearData()
        {
            if (clientData != null)
            {
                clientData.Clear();
            }
            if (clientData2 != null)
            {
                clientData2.Clear();
            }
        }
        public void PopulateData()
        {
            clientData = new ObservableCollection<Company>(_StoredProcedure.GetCompanies());
        }
        public void ButtonAdd()
        {
            clientData2.Add((Company)LeftBox.SelectedItem);
            clientData.Remove((Company)LeftBox.SelectedItem);
        }

        public void ButtonRemove()
        {
            clientData.Add((Company)RightBox.SelectedItem);
            clientData2.Remove((Company)RightBox.SelectedItem);
        }


        public void LoadIntoComoBox(ComboBox yearsBox)
        {
            foreach (var accCard in _StoredProcedure.GetYear())
            {
                if (!yearList.Contains(accCard.Year))
                {
                    yearList.Add(accCard.Year);
                }
            }
            yearList.Sort();
            yearsBox.ItemsSource = yearList;
        }


        public void ExportData()
        {
            ExcelExport export = new ExcelExport();
            string path = export.GetExportPath(RightBox);
            export.Export(RightBox, StartYear, path, _StoredProcedure.getEmployee());
        }
        #endregion

        #region Private methods
        #endregion

    }
}
