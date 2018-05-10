using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Model;
using RevisionFyn.BI_Pro.Database;

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
        public static CustomizeStartScreenController GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new CustomizeStartScreenController();
            }

            return controllerInstance;
        }

        #region KPI
        public void SetKpiListViewSource(ListView KpiListView)
        {
            StoredProcedure sp = new StoredProcedure();

            KpiListView.ItemsSource = sp.GetKPI();
        }

        public void CastSelectedListViewItem(object selectedItem)
        {
            KpiInstance = (KPI)selectedItem;
        }

        public void LoadListViewValuesToChangeable(TextBox TitleTextBox, TextBox UnitTextBox, ComboBox ColorComboBox, CheckBox IsActiveCheckBox)
        {
            TitleTextBox.Text = KpiInstance.Title;
            UnitTextBox.Text = KpiInstance.Unit;

            ColorComboBox.SelectedIndex = KpiInstance.ColorIndex;
            
            if (KpiInstance.IsActive == "Ja")
            {
                IsActiveCheckBox.IsChecked = true;
            }
            else
            {
                IsActiveCheckBox.IsChecked = false;
            }    
        }

        public void AdjustInitalButtons(Button SaveKpiButton, Button DeleteKpiButton, Button AddKpiButton)
        {
            AddKpiButton.Margin = new Thickness(0,15,16,-9);
            AddKpiButton.SetValue(Grid.RowProperty, 5);
        }

        public void AdjustButtonsAfterSelection(Button SaveKpiButton, Button DeleteKpiButton, Button AddKpiButton)
        {
            AddKpiButton.Margin = new Thickness(0,29,16,-23);
            AddKpiButton.SetValue(Grid.RowProperty, 6);

            SaveKpiButton.Visibility = Visibility.Visible;
            DeleteKpiButton.Visibility = Visibility.Visible;
        }

        public void AddKpiToDB(string kpiTitle, string kpiUnit, ComboBox ColorComboBox)
        {
            if (!String.IsNullOrEmpty(kpiTitle) && !String.IsNullOrEmpty(kpiUnit) && ColorComboBox.SelectedItem != null)
            {
                StoredProcedure sp = new StoredProcedure();

                string kpiColor = ColorComboBox.SelectedItem.ToString().Split(' ')[1];
                int colorIndex = ColorComboBox.SelectedIndex;

                MessageBox.Show(sp.AddKPI(kpiTitle, kpiUnit, kpiColor, colorIndex), "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Angiv venligst titel, enhed og farve", "Mangler information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateKpiInDB(string kpiTitle, string kpiUnit, ComboBox ColorComboBox, string isActive)
        {
            if (!String.IsNullOrEmpty(kpiTitle) && !String.IsNullOrEmpty(kpiUnit) && ColorComboBox.SelectedItem != null)
            {
                StoredProcedure sp = new StoredProcedure();

                string kpiColor = ColorComboBox.SelectedItem.ToString().Split(' ')[1];
                int colorIndex = ColorComboBox.SelectedIndex;

                if (isActive == "True")
                {
                    if (!MaximumNumberOfActiveKpiReached())
                    {
                        MessageBox.Show(sp.UpdateKPI(KpiInstance.ID, kpiTitle, kpiUnit, kpiColor, colorIndex, isActive), "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Det maksimale antal af aktive KPI'er er 3", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(sp.UpdateKPI(KpiInstance.ID, kpiTitle, kpiUnit, kpiColor, colorIndex, isActive), "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Angiv venligst titel, enhed og farve", "Mangler information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteKpiFromDB()
        {
            MessageBoxResult deleteConfirmation = MessageBox.Show("Er du sikker på du vil slette denne KPI?", "Bekræft sletning", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (deleteConfirmation == MessageBoxResult.Yes)
            {
                StoredProcedure sp = new StoredProcedure();

                MessageBox.Show(sp.DeleteKPI(KpiInstance.ID), "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Statistics
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
            comboBox.ItemsSource = companies;
            comboBox.DisplayMemberPath = "CompanyName";
        }

        public void LoadColoursIntoCompanyComboBox(ComboBox comboBox)
        {
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
        private bool MaximumNumberOfActiveKpiReached()
        {
            StoredProcedure sp = new StoredProcedure();

            int numberOfActiveKPI = sp.CountActiveKPI();

            if (numberOfActiveKPI < 3 && numberOfActiveKPI != -1)
            {
                return false;
            }
            
            return true;
        }
        #endregion
    }
}
