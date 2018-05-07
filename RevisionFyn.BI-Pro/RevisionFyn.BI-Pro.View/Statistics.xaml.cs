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
        StatisticsController controller;

        public Statistics()
        {
            InitializeComponent();

            controller = StatisticsController.GetInstance(ListOfCompaniesToBeChosen,ListOfChosenCompanies);
            controller.ComboBoxYear();
            controller.LoadIntoComoBox(ComboStartYear);
            controller.LoadIntoComoBox(ComboEndYear);
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

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            controller.ButtonTest(ListOfCompaniesToBeChosen);
            
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            controller.ButtonAdd();

            
        }

        private void ListOfChosenCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            controller.ButtonRemove();
        }



        private void ListOfCompaniesToBeChosen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboEndYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
