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
            //controller.InitializeStep1(StatisticsTypeStackPanel);
            controller.InitializeStep2(DefaultCompaniesListBox);
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
    }
}
