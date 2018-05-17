﻿using System;
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
        public StoredProcedure _StoreProcedure { get; set; }

        public List<int> yearList = new List<int>();
        public ListBox LeftBox { get; set; }
        public ListBox RightBox { get; set; }
        public ComboBox StartYear { get; set; }
        ObservableCollection<Company> companyData;
        ObservableCollection<Company> companyData2;
        #endregion

        #region Constructor
        private OverviewController(ListBox leftBox, ListBox rightBox, ComboBox startYear)
        {
            StartYear = startYear;
            LeftBox = leftBox;
            RightBox = rightBox;
            companyData2 = new ObservableCollection<Company>();


            _StoreProcedure = new StoredProcedure();
            rightBox.ItemsSource = companyData2;
            leftBox.ItemsSource = companyData;
            rightBox.DisplayMemberPath = "CompanyName";
            leftBox.DisplayMemberPath = "CompanyName";
        }
        #endregion

        #region Public methods
        public List<Employee> getMainEmployee()
        {
            List<Employee> allEmployees = new List<Employee>();
            allEmployees = _StoreProcedure.getEmployee();
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
            companies.ItemsSource = companyData;
            companies.DisplayMemberPath = "CompanyName";
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
            companyData = new ObservableCollection<Company>(_StoreProcedure.GetCompanies());
        }
        public void ButtonAdd()
        {
            companyData2.Add((Company)LeftBox.SelectedItem);
            companyData.Remove((Company)LeftBox.SelectedItem);
        }

        public void ButtonRemove()
        {
            companyData.Add((Company)RightBox.SelectedItem);
            companyData2.Remove((Company)RightBox.SelectedItem);
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
        }


        public void ExportData()
        {
            ExcelExport export = new ExcelExport();
            string path = export.GetExportPath(RightBox);
            export.Export(RightBox, StartYear, path, _StoreProcedure.getEmployee());
        }
        #endregion

        #region Private methods
        #endregion

    }
}
