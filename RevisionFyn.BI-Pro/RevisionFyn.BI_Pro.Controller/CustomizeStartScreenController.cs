using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Model;
using RevisionFyn.BI_Pro.Database;
using System.Windows.Media;

namespace RevisionFyn.BI_Pro.Controller
{
    public class CustomizeStartScreenController
    {
        #region Variables
        public List<Client> companies = new List<Client>();
        private static CustomizeStartScreenController _ControllerInstance { get; set; }
        private StoredProcedure _StoredProcedure { get; set; }
        private KPI _KpiInstance { get; set; }
        private List<CustomStatistics> _CustomStatistics { get; set; }
        #endregion

        #region Constructor
        private CustomizeStartScreenController()
        {
            _StoredProcedure = new StoredProcedure();
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

        public void LoadColorsForGraph(ComboBox color1, ComboBox color2, ComboBox color3)
        {
            List<GraphData> listOfGraphData = _StoredProcedure.GetGraphData();
            if (listOfGraphData.Count != 0)
            {
                color1.SelectedIndex = listOfGraphData[0].ColorIndex;
                color2.SelectedIndex = listOfGraphData[1].ColorIndex;
                color3.SelectedIndex = listOfGraphData[2].ColorIndex;
            }
        }
        public void LoadCompaniesToComboBox(ComboBox dropDownClient1, ComboBox dropDownClient2, ComboBox dropDownClient3)
        {
            List<Client> listOfClients = _StoredProcedure.GetCompanies();
            List<GraphData> listOfData = _StoredProcedure.GetGraphData();
            dropDownClient1.ItemsSource = listOfClients;
            dropDownClient2.ItemsSource = listOfClients;
            dropDownClient3.ItemsSource = listOfClients;
            if (listOfData.Count != 0)
            {
                dropDownClient1.SelectedItem = listOfClients.Where(company => company.ClientName == listOfData[0].Company).FirstOrDefault();
                dropDownClient2.SelectedItem = listOfClients.Where(company => company.ClientName == listOfData[1].Company).FirstOrDefault();
                dropDownClient3.SelectedItem = listOfClients.Where(company => company.ClientName == listOfData[2].Company).FirstOrDefault();
            }
            dropDownClient1.DisplayMemberPath = "ClientName";
            dropDownClient2.DisplayMemberPath = "ClientName";
            dropDownClient3.DisplayMemberPath = "ClientName";
        }
        public void LoadYearToComboBox(ComboBox dropDownYear1, ComboBox dropDownYear2, ComboBox dropDownYear3)
        {
            List<int> yearList1 = new List<int>();
            List<int> yearList2 = new List<int>();
            List<int> yearList3 = new List<int>();
            List<GraphData> dataList = _StoredProcedure.GetGraphData();
            foreach (var accCard in _StoredProcedure.GetYear())
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
            dropDownYear1.ItemsSource = yearList1;
            dropDownYear2.ItemsSource = yearList2;
            dropDownYear3.ItemsSource = yearList3;
            if (dropDownYear1.Name == "dropDownStartYear1")
            {
                if (dataList.Count != 0)
                {
                    dropDownYear1.SelectedItem = dataList[0].StartYear;
                    dropDownYear2.SelectedItem = dataList[1].StartYear;
                    dropDownYear3.SelectedItem = dataList[2].StartYear;
                }
            }
            if (dropDownYear1.Name == "dropDownEndYear1")
            {
                if (dataList.Count != 0)
                {
                    dropDownYear1.SelectedItem = dataList[0].LastYear;
                    dropDownYear2.SelectedItem = dataList[1].LastYear;
                    dropDownYear3.SelectedItem = dataList[2].LastYear;
                }
            }
        }

        public void clearGraph()
        {
            _StoredProcedure.ClearGraphData();
        }

        public void SaveButton(ComboBox client, ComboBox startYear, ComboBox lastYear, ComboBox color)
        {
            _StoredProcedure.AddGraphData((Client)client.SelectedItem, (int)startYear.SelectedItem, (int)lastYear.SelectedItem, color.SelectedItem.ToString().Split(' ')[1], color.SelectedIndex);
        }

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
                StoredProcedure sp = new StoredProcedure();

                string kpiColor = ColorComboBox.SelectedItem.ToString().Split(' ')[1];
                int colorIndex = ColorComboBox.SelectedIndex;
                int selectedDataID = _CustomStatistics.Find(x => x.Name == ((CustomStatistics)DataComboBox.SelectedItem).Name).ID;

                MessageBox.Show(sp.AddKPI(kpiTitle, kpiUnit, kpiColor, colorIndex, selectedDataID), "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                StoredProcedure sp = new StoredProcedure();

                string kpiColor = ColorComboBox.SelectedItem.ToString().Split(' ')[1];
                int colorIndex = ColorComboBox.SelectedIndex;
                int selectedDataID = _CustomStatistics.Find(x => x.Name == ((CustomStatistics)DataComboBox.SelectedItem).Name).ID;

                // TO DO: Update StatisticsFavoriteClientMap, if data is changed

                if (isActive == "True")
                {
                    if (!MaximumNumberOfActiveKpiReached())
                    {
                        MessageBox.Show(sp.UpdateKPI(_KpiInstance.ID, kpiTitle, kpiUnit, kpiColor, colorIndex, isActive, selectedDataID), "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Det maksimale antal af aktive KPI'er er 3", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(sp.UpdateKPI(_KpiInstance.ID, kpiTitle, kpiUnit, kpiColor, colorIndex, isActive, selectedDataID), "", MessageBoxButton.OK, MessageBoxImage.Information);
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

        #region Statistics
        public void CreateYearsArray()
        {
            List<double> placeHolder = new List<double>();
            for (int company = 0; company < companies.Count; company++)
            {
                placeHolder.Clear();
                for (int i = companies[company].ClientStartYear - 1; i < companies[company].ClientEndYear; i++)
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
            Client Comp1 = new Client
            {
                ClientName = "Firma1",
                ClientStartYear = 2012,
                ClientEndYear = 2017,
                Coverages = Comp1Coverage,
                y = Comp1Coverage.ToArray()
            };
            Client Comp2 = new Client
            {
                ClientName = "Firma2",
                ClientStartYear = 2012,
                ClientEndYear = 2017,
                Coverages = Comp2Coverage,
                y = Comp2Coverage.ToArray()
            };
            Client Comp3 = new Client
            {
                ClientName = "Firma3",
                ClientStartYear = 2012,
                ClientEndYear = 2017,
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
            comboBox.DisplayMemberPath = "ClientName";
        }

        public void LoadColoursIntoCompanyComboBox(ComboBox comboBox)
        {
            comboBox.ItemsSource = companies;
            comboBox.DisplayMemberPath = "ClientName";
        }

        public void LoadValuesIntoCompanyStartYearBox(ComboBox comboBox, ComboBox companyBox)
        {
            if (companyBox.SelectedItem != null)
            {
                Client comp = (Client)companyBox.SelectedItem;
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
                Client comp = (Client)companyBox.SelectedItem;
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
