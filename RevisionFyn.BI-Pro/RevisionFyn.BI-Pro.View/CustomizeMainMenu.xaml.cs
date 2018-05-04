﻿using System;
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
    /// Interaction logic for CustomizeMainMenu.xaml
    /// </summary>
    public partial class CustomizeMainMenu : Page
    {
        CustomizeStartScreenController controller = CustomizeStartScreenController.GetInstance();
        public CustomizeMainMenu()
        {
            InitializeComponent();
            controller.CreateGraphValues();
            controller.LoadValuesIntoCompanyComboBox(dropDownComp1);
            controller.LoadValuesIntoCompanyComboBox(dropDownComp2);
            controller.LoadValuesIntoCompanyComboBox(dropDownComp3);
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainMenu.Xaml", UriKind.Relative));
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("OverView.Xaml", UriKind.Relative));
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Statistics.Xaml", UriKind.Relative));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dropDownComp1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            controller.LoadValuesIntoCompanyStartYearBox(dropDownStartYear1, dropDownComp1);
            controller.LoadValuesIntoCompanyStartYearBox(dropDownEndYear1, dropDownComp1);
        }

        private void dropDownComp3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            controller.LoadValuesIntoCompanyStartYearBox(dropDownStartYear2, dropDownComp2);
            controller.LoadValuesIntoCompanyStartYearBox(dropDownEndYear2, dropDownComp2);
        }

        private void dropDownComp2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            controller.LoadValuesIntoCompanyStartYearBox(dropDownStartYear3, dropDownComp3);
            controller.LoadValuesIntoCompanyStartYearBox(dropDownEndYear3, dropDownComp1);
        }
    }
}