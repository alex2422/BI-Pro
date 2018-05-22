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
        public StoredProcedure _StoredProcedure { get; set; }

        public List<int> yearList = new List<int>();
        ObservableCollection<Client> clientData;
        ObservableCollection<Client> clientData2;
        #endregion

        #region Constructor
        private OverviewController()
        {
            _StoredProcedure = new StoredProcedure();
        }
        #endregion

        #region Public methods
        public List<Employee> getMainEmployee()
        {
            List<Employee> allEmployees = new List<Employee>();
            allEmployees = _StoredProcedure.getEmployee();
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
            leftBox.ItemsSource = clientData;
            leftBox.DisplayMemberPath = "ClientName";
            rightBox.ItemsSource = clientData2;
            rightBox.DisplayMemberPath = "ClientName";
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
            clientData = new ObservableCollection<Client>(_StoredProcedure.GetClient());
            clientData2 = new ObservableCollection<Client>();
        }
        public void ButtonAdd(ListBox LeftBox)
        {
            clientData2.Add((Client)LeftBox.SelectedItem);
            clientData.Remove((Client)LeftBox.SelectedItem);
        }

        public void ButtonRemove(ListBox RightBox)
        {
            clientData.Add((Client)RightBox.SelectedItem);
            clientData2.Remove((Client)RightBox.SelectedItem);
        }

        public void ButtonAddAll(ListBox LeftBox)
        {
            int timesToDo = clientData.Count();
            for (int i = 0; i < timesToDo; i++)
            {
                clientData2.Add((Client)clientData[0]);
                clientData.Remove((Client)clientData[0]);
            }
        }

        public void ButtonRemoveAll(ListBox RightBox)
        {
            int timesToDo = clientData2.Count();
            for (int i = 0; i < timesToDo; i++)
            {
                clientData.Add((Client)clientData2[0]);
                clientData2.Remove((Client)clientData2[0]);
            }
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
                    export.Export(rightBox, StartYear, path, _StoredProcedure.getEmployee());
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
