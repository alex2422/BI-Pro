using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Model;
using RevisionFyn.BI_Pro.Database;

namespace RevisionFyn.BI_Pro.Controller
{
    public class CustomizeStartScreenController
    {
        #region Variables / Properties
        private static CustomizeStartScreenController _ControllerInstance { get; set; }
        private List<GraphData> _GraphData { get; set; }
        private StoredProcedure _StoredProcedure { get; set; }
        private KPI _KpiInstance { get; set; }
        private List<CustomStatistics> _CustomStatistics { get; set; }
        #endregion

        #region Constructor
        private CustomizeStartScreenController()
        {
            _StoredProcedure = new StoredProcedure();
            _GraphData = _StoredProcedure.GetGraphData();
        }
        #endregion

        #region Public Methods
        public static CustomizeStartScreenController GetInstance()
        {
            if (_ControllerInstance == null)
            {
                _ControllerInstance = new CustomizeStartScreenController();
            }

            return _ControllerInstance;
        }

        #region Graph
        public void LoadColorsForGraph(ComboBox Color1ComboBox, ComboBox Color2ComboBox, ComboBox Color3ComboBox)
        {
            if (_GraphData.Count != 0)
            {
                Color1ComboBox.SelectedIndex = _GraphData[0].ColorIndex;
                Color2ComboBox.SelectedIndex = _GraphData[1].ColorIndex;
                Color3ComboBox.SelectedIndex = _GraphData[2].ColorIndex;
            }
        }

        public void LoadClientsToComboBox(ComboBox Client1ComboBox, ComboBox Client2ComboBox, ComboBox Client3ComboBox)
        {
            List<Client> clients = _StoredProcedure.GetClient();

            Client1ComboBox.ItemsSource = clients;
            Client2ComboBox.ItemsSource = clients;
            Client3ComboBox.ItemsSource = clients;

            if (_GraphData.Count != 0)
            {
                Client1ComboBox.SelectedItem = clients.Where(company => company.ClientName == _GraphData[0].Client).FirstOrDefault();
                Client2ComboBox.SelectedItem = clients.Where(company => company.ClientName == _GraphData[1].Client).FirstOrDefault();
                Client3ComboBox.SelectedItem = clients.Where(company => company.ClientName == _GraphData[2].Client).FirstOrDefault();
            }

            Client1ComboBox.DisplayMemberPath = "ClientName";
            Client2ComboBox.DisplayMemberPath = "ClientName";
            Client3ComboBox.DisplayMemberPath = "ClientName";
        }

        public void LoadYearsToComboBox(ComboBox Year1ComboBox, ComboBox Year2ComboBox, ComboBox Year3ComboBox)
        {
            List<int> yearList1 = new List<int>();
            List<int> yearList2 = new List<int>();
            List<int> yearList3 = new List<int>();

            foreach (AccountCard accCard in _StoredProcedure.GetYear())
            {
                if (!yearList1.Contains(accCard.Year))
                {
                    yearList1.Add(accCard.Year);
                }
                if (!yearList2.Contains(accCard.Year))
                {
                    yearList2.Add(accCard.Year);
                }
                if (!yearList3.Contains(accCard.Year))
                {
                    yearList3.Add(accCard.Year);
                }
            }

            yearList1.Sort();
            yearList2.Sort();
            yearList3.Sort();
            Year1ComboBox.ItemsSource = yearList1;
            Year2ComboBox.ItemsSource = yearList2;
            Year3ComboBox.ItemsSource = yearList3;

            if (Year1ComboBox.Name == "Year1ComboBox")
            {
                if (_GraphData.Count != 0)
                {
                    Year1ComboBox.SelectedItem = _GraphData[0].StartYear;
                    Year2ComboBox.SelectedItem = _GraphData[1].StartYear;
                    Year3ComboBox.SelectedItem = _GraphData[2].StartYear;
                }
            }

            if (Year1ComboBox.Name == "EndYear1ComboBox")
            {
                if (_GraphData.Count != 0)
                {
                    Year1ComboBox.SelectedItem = _GraphData[0].LastYear;
                    Year2ComboBox.SelectedItem = _GraphData[1].LastYear;
                    Year3ComboBox.SelectedItem = _GraphData[2].LastYear;
                }
            }
        }

        public void ClearGraph()
        {
            _StoredProcedure.ClearGraphData();
        }

        public void SaveGraphButton(ComboBox ClientComboBox, ComboBox StartYearComboBox, ComboBox EndYearComboBox, ComboBox ColorComboBox)
        {
            _StoredProcedure.AddGraphData((Client)ClientComboBox.SelectedItem, (int)StartYearComboBox.SelectedItem, (int)EndYearComboBox.SelectedItem,
                ColorComboBox.SelectedItem.ToString().Split(' ')[1], ColorComboBox.SelectedIndex);
        }
        #endregion

        #region KPI
        public void SetKpiListViewSource(ListView KpiListView)
        {
            KpiListView.ItemsSource = _StoredProcedure.GetKPI();
        }

        public void LoadValuesIntoKpiDataComboBox(ComboBox DataComboBox)
        {
            _CustomStatistics = _StoredProcedure.GetActiveStatisticsFavorite();

            DataComboBox.ItemsSource = _CustomStatistics;
            DataComboBox.DisplayMemberPath = "Name";
        }

        public void CastSelectedListViewItem(object selectedItem)
        {
            _KpiInstance = (KPI)selectedItem;
        }

        public void LoadListViewValuesToChangeable(TextBox TitleTextBox, TextBox UnitTextBox, ComboBox ColorComboBox, ComboBox DataComboBox, CheckBox IsActiveCheckBox)
        {
            TitleTextBox.Text = _KpiInstance.Title;
            UnitTextBox.Text = _KpiInstance.Unit;

            ColorComboBox.SelectedIndex = _KpiInstance.ColorIndex;

            DataComboBox.SelectedItem = _CustomStatistics.Find(x => x.ID == _KpiInstance.DataID);
            
            if (_KpiInstance.IsActive == "Ja")
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

        public void AddKpiToDB(string kpiTitle, string kpiUnit, ComboBox DataComboBox, ComboBox ColorComboBox)
        {
            if (!String.IsNullOrEmpty(kpiTitle) && !String.IsNullOrEmpty(kpiUnit) && DataComboBox.SelectedItem != null && ColorComboBox.SelectedItem != null)
            {
                string kpiColor = ColorComboBox.SelectedItem.ToString().Split(' ')[1];
                int colorIndex = ColorComboBox.SelectedIndex;
                int selectedDataID = _CustomStatistics.Find(x => x.Name == ((CustomStatistics)DataComboBox.SelectedItem).Name).ID;

                MessageBox.Show(_StoredProcedure.AddKPI(kpiTitle, kpiUnit, kpiColor, colorIndex, selectedDataID), "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Angiv venligst titel, enhed, data og farve", "Mangler information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateKpiInDB(string kpiTitle, string kpiUnit, ComboBox ColorComboBox, string isActive, ComboBox DataComboBox)
        {
            if (!String.IsNullOrEmpty(kpiTitle) && !String.IsNullOrEmpty(kpiUnit) && ColorComboBox.SelectedItem != null)
            {
                string kpiColor = ColorComboBox.SelectedItem.ToString().Split(' ')[1];
                int colorIndex = ColorComboBox.SelectedIndex;
                int selectedDataID = _CustomStatistics.Find(x => x.Name == ((CustomStatistics)DataComboBox.SelectedItem).Name).ID;

                if (isActive == "True")
                {
                    if (!MaximumNumberOfActiveKpiReached())
                    {
                        MessageBox.Show(_StoredProcedure.UpdateKPI(_KpiInstance.ID, kpiTitle, kpiUnit, kpiColor, colorIndex, isActive, selectedDataID), "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Det maksimale antal af aktive KPI'er er 3", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(_StoredProcedure.UpdateKPI(_KpiInstance.ID, kpiTitle, kpiUnit, kpiColor, colorIndex, isActive, selectedDataID), "", MessageBoxButton.OK, MessageBoxImage.Information);
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

                MessageBox.Show(sp.DeleteKPI(_KpiInstance.ID), "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #endregion

        #region Private Methods
        private bool MaximumNumberOfActiveKpiReached()
        {
            int numberOfActiveKPI = _StoredProcedure.CountActiveKPI();

            if (numberOfActiveKPI < 3 && numberOfActiveKPI != -1)
            {
                return false;
            }
            
            return true;
        }
        #endregion
    }
}
