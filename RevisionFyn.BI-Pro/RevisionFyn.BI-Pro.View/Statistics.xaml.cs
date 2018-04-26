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

namespace RevisionFyn.BI_Pro.View
{
    /// <summary>
    /// Interaction logic for Statisticks.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private void StartScreenButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainMenu.Xaml", UriKind.Relative));
        }

        private void OverView_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Overview.Xaml", UriKind.Relative));
        }
    }
}
