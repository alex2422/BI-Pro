using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RevisionFyn.BI_Pro.Controller;

namespace RevisionFyn.BI_Pro.View
{
    /// <summary>
    /// Interaction logic for Statisticks.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        StatisticsController controller = StatisticsController.GetInstance();
        
        public Statistics()
        {
            InitializeComponent();

            List<Control> step2Controls = new List<Control>
            {
                DefaultCompaniesListBox,
                StatisticsCalculationComboBox
            };

            controller.InitializeSteps(Step1Grid, step2Controls, Step2Grid, Step3Grid, ProgressGrid);
            controller.LoadStep1(StatisticsTypeStackPanel);
        }

        private void StartScreenButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainMenu.Xaml", UriKind.Relative));
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Overview.Xaml", UriKind.Relative));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void AddSelectedCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            controller.MoveItemsToListBox(SelectedCompanesListBox, DefaultCompaniesListBox);
        }

        private void RemoveSelectedCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            controller.MoveItemsToListBox(DefaultCompaniesListBox, SelectedCompanesListBox);
        }

        private void Step1CircleImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            controller.ShowSelectedStep(Step1Grid);
        }

        private void Step2CircleImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            controller.ShowSelectedStep(Step2Grid);
        }

        private void Step3CircleImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            controller.LoadStep3(StatisticsCalculationComboBox, SelectedCompanesListBox);
            controller.ShowSelectedStep(Step3Grid);
        }

        private void AddStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            controller.SaveCustomStatistics(FavoriteNameTextBox);
        }
    }
}
