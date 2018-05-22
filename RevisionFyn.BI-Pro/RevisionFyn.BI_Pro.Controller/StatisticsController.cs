using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Database;
using RevisionFyn.BI_Pro.Model;
using System.Windows.Media.Imaging;

namespace RevisionFyn.BI_Pro.Controller
{
    public class StatisticsController
    {
        #region Variables / Properties
        private static StatisticsController _ControllerInstance { get; set; }
        private StoredProcedure _StoredProcedure { get; set; }
        private Grid _Step1Grid { get; set; }
        private Grid _Step2Grid { get; set; }
        private Grid _Step3Grid { get; set; }
        private Grid _ProgressGrid { get; set; }
        private List<Control> _Step2Controls { get; set; }
        private bool _IsStep1Done { get; set; }
        private bool _IsStep2Done { get; set; }
        private bool _IsStep3Done { get; set; }
        private CustomStatistics _CustomStatistics { get; set; }
        private List<StatisticsCalculation> _ListOfActiveCalculations { get; set; }
        private List<Client> _ListOfCompanies { get; set; }
        #endregion

        #region Constructor
        private StatisticsController()
        {
            _StoredProcedure = new StoredProcedure();
            _CustomStatistics = new CustomStatistics();
        }
        #endregion

        #region Public methods
        public static StatisticsController GetInstance()
        {
            if (_ControllerInstance == null)
            {
                _ControllerInstance = new StatisticsController();
            }

            return _ControllerInstance;
        }

        public void InitializeSteps(Grid Step1Grid, List<Control> step2Controls, Grid Step2Grid, Grid Step3Grid, Grid ProgressGrid)
        {
            _Step1Grid = Step1Grid;
            _Step2Controls = step2Controls;
            _Step2Grid = Step2Grid;
            _Step3Grid = Step3Grid;
            _ProgressGrid = ProgressGrid;

            _IsStep1Done = false;
            _IsStep2Done = false;
            _IsStep3Done = false;
        }

        public void LoadStep1(StackPanel StatisticsTypeStackPanel)
        {
            LoadButtonsIntoStackPanel(StatisticsTypeStackPanel, _StoredProcedure.GetActiveStatisticsType());
            UpdateProgress(_ProgressGrid, 0);
            _IsStep1Done = true;
        }

        public void LoadStep2(ListBox DefaultCompaniesListBox, ComboBox StatisticsCalculationComboBox)
        {
            foreach (Control control in _Step2Controls)
            {
                if (control is ListBox)
                {
                    DefaultCompaniesListBox = (ListBox)control;
                }

                if (control is ComboBox)
                {
                    StatisticsCalculationComboBox = (ComboBox)control;
                }
            }

            _ListOfActiveCalculations = _StoredProcedure.GetActiveStatisticsCalculation();
            _ListOfCompanies = _StoredProcedure.GetClient();

            AddStatisticsCalculationToComboBox(StatisticsCalculationComboBox, _ListOfActiveCalculations);
            AddCompanyToListBox(DefaultCompaniesListBox, _ListOfCompanies);

            _IsStep2Done = true;
        }

        public void LoadStep3(ComboBox StatisticsCalculationComboBox, ListBox SelectedCompanesListBox)
        {
            if (StatisticsCalculationComboBox.SelectedIndex != -1)
            {
                _CustomStatistics.ChoosenStatisticsCalculationID = _ListOfActiveCalculations.Find(x => x.Name == StatisticsCalculationComboBox.SelectedValue.ToString()).ID;
            }

            _CustomStatistics.ChoosenCompanies = GetSelectedCompanies(SelectedCompanesListBox);

            _IsStep3Done = true;
        }

        public void UpdateProgress(Grid ProgressGrid, int stepID)
        {
            switch (stepID)
            {
                case 0:
                    if (FindObjectByName(ProgressGrid, "Step1CircleImage") is Image Step1CircleImage)
                    {
                        Step1CircleImage.Source = new BitmapImage(new Uri("Images/DoneCircle.png", UriKind.Relative));
                    }
                    break;
                case 1:
                    if (FindObjectByName(ProgressGrid, "Step1LineImage") is Image Step1LineImage)
                    {
                        Step1LineImage.Source = new BitmapImage(new Uri("Images/DoneLine.png", UriKind.Relative));
                    }
                    if (FindObjectByName(ProgressGrid, "Step2CircleImage") is Image Step2CircleImage)
                    {
                        Step2CircleImage.Source = new BitmapImage(new Uri("Images/DoneCircle.png", UriKind.Relative));
                    }
                    break;
                case 2:
                    if (FindObjectByName(ProgressGrid, "Step2LineImage") is Image Step2LineImage)
                    {
                        Step2LineImage.Source = new BitmapImage(new Uri("Images/DoneLine.png", UriKind.Relative));
                    }
                    if (FindObjectByName(ProgressGrid, "Step3CircleImage") is Image Step3CircleImage)
                    {
                        Step3CircleImage.Source = new BitmapImage(new Uri("Images/DoneCircle.png", UriKind.Relative));
                    }
                    break;
                default:
                    break;
            }
        }

        public void MoveItemsToListBox(ListBox ToAddListBox, ListBox ToRemoveListBox)
        {
            if (ToRemoveListBox.SelectedItem != null)
            {
                if (!ToAddListBox.Items.Contains(ToRemoveListBox.SelectedValue))
                {
                    ToAddListBox.Items.Add(ToRemoveListBox.SelectedValue);
                    ToRemoveListBox.Items.Remove(ToRemoveListBox.SelectedValue);
                } 
            } 
        }

        public void ShowSelectedStep(Grid SelectedGrid)
        {
            if (SelectedGrid.Name.Contains("Step1") && _IsStep1Done)
            {
                _Step2Grid.Visibility = Visibility.Hidden;
                _Step3Grid.Visibility = Visibility.Hidden;

                _Step1Grid.Visibility = Visibility.Visible;
                UpdateProgress(_ProgressGrid, 0);
            }
            else if (SelectedGrid.Name.Contains("Step2") && _IsStep2Done)
            {
                _Step1Grid.Visibility = Visibility.Hidden;
                _Step3Grid.Visibility = Visibility.Hidden;

                _Step2Grid.Visibility = Visibility.Visible;
                UpdateProgress(_ProgressGrid, 1);
            }
            else if (SelectedGrid.Name.Contains("Step3") && _IsStep2Done && _IsStep3Done)
            {
                _Step1Grid.Visibility = Visibility.Hidden;
                _Step2Grid.Visibility = Visibility.Hidden;

                _Step3Grid.Visibility = Visibility.Visible;
                UpdateProgress(_ProgressGrid, 2);
            }
        }

        public void SaveCustomStatistics(TextBox FavoriteNameTextBox)
        {
            _CustomStatistics.Name = FavoriteNameTextBox.Text;

            if (!String.IsNullOrEmpty(_CustomStatistics.Name) && _CustomStatistics.ChoosenStatisticsTypeID != 0 && _CustomStatistics.ChoosenStatisticsCalculationID != 0)
            {
                MessageBox.Show(_StoredProcedure.AddStatisticsFavorite(_CustomStatistics.Name, _CustomStatistics.ChoosenStatisticsTypeID,
                _CustomStatistics.ChoosenStatisticsCalculationID));
            }

            AddSelectedCompaniesToMap();
        }
        #endregion

        #region Private methods

        private List<Client> GetSelectedCompanies(ListBox SelectedCompanesListBox)
        {
            List<Client> selectedCompanies = new List<Client>();

            foreach (string item in SelectedCompanesListBox.Items)
            {
                selectedCompanies.Add(_ListOfCompanies.Find(x => x.ClientName == item));
            }

            return selectedCompanies;
        }

        private void AddSelectedCompaniesToMap()
        {
            foreach (Client c in _CustomStatistics.ChoosenCompanies)
            {
                _StoredProcedure.AddStatisticsFavoriteClientMap(c.ClientID);
            }
        }

        private void LoadButtonsIntoStackPanel(StackPanel StatisticsTypeStackPanel, List<StatisticsType> activeStatisticsType)
        {
            foreach (StatisticsType st in activeStatisticsType)
            {
                Button typeChooseButton = new Button
                {
                    Name = String.Format("TypeChoose{0}Button", st.ID),
                    Content = st.Name,
                    Margin = new Thickness(10),
                    FontSize = 30,
                    Tag = st.ID
                };

                typeChooseButton.Click += TypeChooseButton_Click;

                StatisticsTypeStackPanel.Children.Add(typeChooseButton);
            }
        }

        private void TypeChooseButton_Click(object sender, RoutedEventArgs e)
        {
            Button statisticsTypeSender = (Button)sender;
            _CustomStatistics.ChoosenStatisticsTypeID = (int)statisticsTypeSender.Tag;

            _Step1Grid.Visibility = Visibility.Hidden;
            _Step2Grid.Visibility = Visibility.Visible;

            LoadStep2(null, null);

            UpdateProgress(_ProgressGrid, 1);
        }

        private object FindObjectByName(Grid GridToSearch, string objectName)
        {
            return GridToSearch.FindName(objectName);
        }

        private void AddStatisticsCalculationToComboBox(ComboBox StatisticsCalculationComboBox, List<StatisticsCalculation> listOfActiveCalculations)
        {
            StatisticsCalculationComboBox.Items.Clear();

            foreach (StatisticsCalculation sc in listOfActiveCalculations)
            {
                StatisticsCalculationComboBox.Items.Add(sc.Name);
            }
        }

        private void AddCompanyToListBox(ListBox DefaultCompaniesListBox, List<Client> listOfCompanies)
        {
            DefaultCompaniesListBox.Items.Clear();

            foreach (Client company in listOfCompanies)
            {
                DefaultCompaniesListBox.Items.Add(company.ClientName);
            }
        }
        #endregion
    }
}
