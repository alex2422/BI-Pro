using System;
using System.Windows;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Controller;

namespace RevisionFyn.BI_Pro.View
{
    public partial class MainMenu : Page
    {
        MainMenuController controller = MainMenuController.GetInstance();

        public MainMenu()
        {
            InitializeComponent();
            controller.CreateKpiElements(KpiGrid);
            controller.CreateGraph(linesGrid, Client1Label, Client2Label, Client3Label);
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Overview.Xaml", UriKind.Relative));
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Statistics.Xaml", UriKind.Relative));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CustomizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("CustomizeMainMenu.Xaml", UriKind.Relative));
        }
    }
}
