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

namespace RevisionFyn.BI_Pro.Controller
{
    public class StatisticsController
    {
        #region Variables / Properties
        private static StatisticsController _ControllerInstance { get; set; }
        private StoredProcedure _StoredProcedure { get; set; }
        private Grid _Step1Grid { get; set; }
        private Grid _Step2Grid { get; set; }
        private List<Control> _Step2Controls { get; set; }
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

        public void LoadStep1(Grid Step1Grid, StackPanel StatisticsTypeStackPanel, List<Control> Step2Controls, Grid Step2Grid)
        {
            _Step1Grid = Step1Grid;
            _Step2Controls = Step2Controls;
            _Step2Grid = Step2Grid;

            LoadButtonsIntoStackPanel(StatisticsTypeStackPanel, _StoredProcedure.GetActiveStatisticsType()); 
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
        }

        public void MoveItemsToListBox(ListBox ToAddListBox, ListBox ToRemoveListBox)
        {
            if (ToRemoveListBox.SelectedItem != null)
            {
                ToAddListBox.Items.Add(ToRemoveListBox.SelectedValue);
                ToRemoveListBox.Items.Remove(ToRemoveListBox.SelectedValue);
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
                //typeChooseButton.Click += delegate(object sender, RoutedEventArgs e) { TypeChooseButton_Click(sender, e, null); };

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
            foreach (StatisticsCalculation sc in listOfActiveCalculations)
            {
                StatisticsCalculationComboBox.Items.Add(sc.Name);
            }
        }

        private void AddCompanyToListBox(ListBox DefaultCompaniesListBox, List<Company> listOfCompanies)
        {
            foreach (Company company in listOfCompanies)
            {
                DefaultCompaniesListBox.Items.Add(company.CompanyName);
            }
        }
        #endregion
    }
}
