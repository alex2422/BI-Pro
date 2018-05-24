using System;
using System.Windows;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Controller;

namespace RevisionFyn.BI_Pro.View
{
    public partial class Overview : Page
    {
        OverviewController controller =  OverviewController.GetInstance();

        public Overview()
        {
            InitializeComponent();

            controller.ClearData();
            controller.GetDataFromDB();
            controller.LoadIntoListBox(ClientsToBeChosenListBox, ChoosenClientsListBox);
            controller.LoadValuesIntoComoBox(StartYearComboBox);
            controller.LoadValuesIntoComoBox(EndYearComboBox);
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            controller.ButtonAdd(ClientsToBeChosenListBox);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            controller.ButtonRemove(ChoosenClientsListBox);
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            controller.ExportData(ChoosenClientsListBox, StartYearComboBox);
        }

        private void RemoveAllButton_Click(object sender, RoutedEventArgs e)
        {
            controller.RemoveAllButton();
        }

        private void AddAllButton_Click(object sender, RoutedEventArgs e)
        {
            controller.AddAllButton();
        }
    }
}
