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
        private Grid _ProgressGrid { get; set; }
        private List<Control> _Step2Controls { get; set; }
        private bool _IsStep1Done { get; set; }
        private bool _IsStep2Done { get; set; }
        #endregion

        #region Constructor
        private StatisticsController()
        {
            _StoredProcedure = new StoredProcedure();
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

        public void InitializeSteps(Grid Step1Grid, List<Control> step2Controls, Grid Step2Grid, Grid ProgressGrid)
        {
            _Step1Grid = Step1Grid;
            _Step2Controls = step2Controls;
            _Step2Grid = Step2Grid;
            _ProgressGrid = ProgressGrid;

            _IsStep1Done = false;
            _IsStep2Done = false;
        }

        public void LoadStep1(StackPanel StatisticsTypeStackPanel)
        {
            LoadButtonsIntoStackPanel(StatisticsTypeStackPanel, _StoredProcedure.GetActiveStatisticsType());
            UpdateProgress(_ProgressGrid, 0);
            _IsStep1Done = true;
        }

        public void LoadStep2(ListBox DefaultCompaniesListBox, ComboBox StatisticsCalculationComboBox)
        {
            //InitializeStep2Controls(DefaultCompaniesListBox, StatisticsCalculationComboBox);

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

            List<StatisticsCalculation> listOfActiveCalculations = _StoredProcedure.GetActiveStatisticsCalculation();
            List<Company> listOfCompanies = _StoredProcedure.GetCompanies();

            AddStatisticsCalculationToComboBox(StatisticsCalculationComboBox, listOfActiveCalculations);
            AddCompanyToListBox(DefaultCompaniesListBox, listOfCompanies);

            _IsStep2Done = true;
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

                _Step1Grid.Visibility = Visibility.Visible;
                UpdateProgress(_ProgressGrid, 0);
            }
            else if (SelectedGrid.Name.Contains("Step2") && _IsStep2Done)
            {
                _Step1Grid.Visibility = Visibility.Hidden;

                _Step2Grid.Visibility = Visibility.Visible;
                UpdateProgress(_ProgressGrid, 1);
            }
        }
        #endregion

        #region Private methods
        private void LoadButtonsIntoStackPanel(StackPanel StatisticsTypeStackPanel, List<StatisticsType> activeStatisticsType)
        {
            foreach (StatisticsType st in activeStatisticsType)
            {
                Button typeChooseButton = new Button
                {
                    Name = String.Format("TypeChoose{0}Button", st.ID),
                    Content = st.Name,
                    Margin = new Thickness(10),
                    FontSize = 30 
                };

                typeChooseButton.Click += TypeChooseButton_Click;

                StatisticsTypeStackPanel.Children.Add(typeChooseButton);
            }
        }

        private void TypeChooseButton_Click(object sender, RoutedEventArgs e)
        {
            _Step1Grid.Visibility = Visibility.Hidden;
            _Step2Grid.Visibility = Visibility.Visible;

            LoadStep2(null, null);

            Button statisticsTypeSender = (Button)sender;
            //MessageBox.Show(statisticsTypeSender.Name);

            UpdateProgress(_ProgressGrid, 1);
        }

        private object FindObjectByName(Grid GridToSearch, string objectName)
        {
            return GridToSearch.FindName(objectName);
        }

        private void InitializeStep2Controls(ListBox DefaultCompaniesListBox, ComboBox StatisticsCalculationComboBox)
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
        }

        private void AddStatisticsCalculationToComboBox(ComboBox StatisticsCalculationComboBox, List<StatisticsCalculation> listOfActiveCalculations)
        {
            StatisticsCalculationComboBox.Items.Clear();

            foreach (StatisticsCalculation sc in listOfActiveCalculations)
            {
                StatisticsCalculationComboBox.Items.Add(sc.Name);
            }
        }

        private void AddCompanyToListBox(ListBox DefaultCompaniesListBox, List<Company> listOfCompanies)
        {
            DefaultCompaniesListBox.Items.Clear();

            foreach (Company company in listOfCompanies)
            {
                DefaultCompaniesListBox.Items.Add(company.CompanyName);
            }
        }
        #endregion
    }
}
