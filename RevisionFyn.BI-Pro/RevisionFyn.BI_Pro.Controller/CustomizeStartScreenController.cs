using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Controller
{
    public class CustomizeStartScreenController
    {
        #region Variables
        public List<Company> companies = new List<Company>();
        private static CustomizeStartScreenController controllerInstance;
        public KPI KpiInstance { get; set; }
        #endregion

        #region Constructor
        private CustomizeStartScreenController()
        { }
        #endregion

        #region Public Methods

        #region KPI
        public List<KPI> ListOfKpiFromDB()
        {
            KPI KPI_1 = new KPI
            {
                Title = "Total dækning (+/-)",
                Value = 304612,
                Unit = "DKK",
                Color = "Dodgerblue",
                IsActive = true
            };
            KPI KPI_2 = new KPI
            {
                Title = "Kunder med underdækning",
                Value = 42,
                Unit = "Antal",
                Color = "Red",
                IsActive = true
            };
            KPI KPI_3 = new KPI
            {
                Title = "Gns. dækning (+/-) pr. kunde",
                Value = 5840,
                Unit = "DKK",
                Color = "Black",
                IsActive = false
            };

            List<KPI> listOfKPI = new List<KPI>
            {
                KPI_1,
                KPI_2,
                KPI_3
            };

            return listOfKPI;
        }

        public void SetKpiListViewSource(ListView KpiListView)
        {
            KpiListView.ItemsSource = ListOfKpiFromDB();
        }

        public void CastSelectedListViewItem(object selectedItem)
        {
            KpiInstance = (KPI)selectedItem;
        }

        public void LoadListViewValuesToChangeable(TextBox TitleTextBox, TextBox UnitTextBox, ComboBox ColorComboBox, CheckBox IsActiveCheckBox)
        {
            TitleTextBox.Text = KpiInstance.Title;
            UnitTextBox.Text = KpiInstance.Unit;
            // string s = ColorComboBox.SelectedItem.ToString().Split(' ')[1];

            if (KpiInstance.IsActive)
            {
                IsActiveCheckBox.IsChecked = true;
            }
            else
            {
                IsActiveCheckBox.IsChecked = false;
            }
            
        }
        #endregion

        #region Statistics
        public static CustomizeStartScreenController GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new CustomizeStartScreenController();
            }

            return controllerInstance;
        }
        public void CreateYearsArray()
        {
            List<double> placeHolder = new List<double>();
            for (int company = 0; company < companies.Count; company++)
            {
                placeHolder.Clear();
                for (int i = companies[company].CompanyStartYear - 1; i < companies[company].CompanyEndYear; i++)
                {
                    placeHolder.Add(i);
                }
                companies[company].x = placeHolder.ToArray();
            }
        }

        public void SaveChangesGraph()
        {

        }
        public void CreateGraphValues()
        {
            List<double> Comp1Coverage = new List<double>() { 120, 150, -200, -90, -10, -30};
            List<double> Comp2Coverage = new List<double>() { 110, 140, -210, -100, -20, 20 };
            List<double> Comp3Coverage = new List<double>() { 50, 0, -50, 0, -5, -30};
            Company Comp1 = new Company
            {
                CompanyName = "Firma1",
                CompanyStartYear = 2012,
                CompanyEndYear = 2017,
                Coverages = Comp1Coverage,
                y = Comp1Coverage.ToArray()
            };
            Company Comp2 = new Company
            {
                CompanyName = "Firma2",
                CompanyStartYear = 2012,
                CompanyEndYear = 2017,
                Coverages = Comp2Coverage,
                y = Comp2Coverage.ToArray()
            };
            Company Comp3 = new Company
            {
                CompanyName = "Firma3",
                CompanyStartYear = 2012,
                CompanyEndYear = 2017,
                Coverages = Comp3Coverage,
                y = Comp3Coverage.ToArray()
            };
            companies.Clear();
            companies.Add(Comp1);
            companies.Add(Comp2);
            companies.Add(Comp3);
        }
        public void LoadValuesIntoCompanyComboBox(ComboBox comboBox)
        {
            //    foreach (Company Comp in companies)
            //    {
            //        if (!comboBox.Items.Contains(Comp.CompanyName))
            //        {
            //            comboBox.Items.Add(Comp.CompanyName);
            //        }
            //    }
            comboBox.ItemsSource = companies;
            comboBox.DisplayMemberPath = "CompanyName";
        }
        public void LoadValuesIntoCompanyStartYearBox(ComboBox comboBox, ComboBox companyBox)
        {
            if (companyBox.SelectedItem != null)
            {
                Company comp = (Company)companyBox.SelectedItem;
                for (int i = 0; i < comp.x.Length; i++)
                {
                    if (!comboBox.Items.Contains(comp.x[i]))
                    {
                        comboBox.Items.Add(comp.x[i]);
                    }
                }
            }
        }
        public void LoadValuesIntoCompanyEndYearBox(ComboBox comboBox, ComboBox companyBox)
        {
            if (companyBox.SelectedItem != null)
            {
                Company comp = (Company)companyBox.SelectedItem;
                for (int i = 0; i < comp.x.Length; i++)
                {
                    if (!comboBox.Items.Contains(comp.x[i]))
                    {
                        comboBox.Items.Add(comp.x[i]);
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Private Methods

        #endregion
    }
}
