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
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Page
    {
        OverviewController controller;
        public Overview()
        {
            InitializeComponent();

            controller = OverviewController.GetInstance(ListBoxCompaniesToBeChosen, ListBoxChosenCampanies, ComboBoxStartYear);
            //controller.ComboBoxYear();
            controller.ClearData();
            controller.PopulateData();
            controller.LoadIntoListBox(ListBoxCompaniesToBeChosen);
            controller.LoadIntoComoBox(ComboBoxStartYear);
            controller.LoadIntoComoBox(ComboBoxEndYear);
            
        }
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainMenu.Xaml", UriKind.Relative));
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Statistics.Xaml", UriKind.Relative));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
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



        private void ListOfCompaniesToBeChosen(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboEndYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            controller.ExportData();
        }
    }
}
