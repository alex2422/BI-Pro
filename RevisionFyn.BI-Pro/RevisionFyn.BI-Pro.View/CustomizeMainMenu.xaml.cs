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
    /// Interaction logic for CustomizeMainMenu.xaml
    /// </summary>
    public partial class CustomizeMainMenu : Page
    {
        CustomizeStartScreenController controller = CustomizeStartScreenController.GetInstance();

        public CustomizeMainMenu()
        {
            InitializeComponent();
            controller.SetKpiListViewSource(KpiListVIew);
            controller.LoadValuesIntoKpiDataComboBox(DataComboBox);
            controller.AdjustInitalButtons(SaveKpiButton, DeleteKpiButton, AddKpiButton);
            controller.CreateGraphValues();
            controller.CreateYearsArray();
            controller.LoadCompaniesToComboBox(dropDownComp1, dropDownComp2, dropDownComp3);
            controller.LoadYearToComboBox(dropDownStartYear1, dropDownStartYear2, dropDownStartYear3);
            controller.LoadYearToComboBox(dropDownEndYear1, dropDownEndYear2, dropDownEndYear3);
            ColorComboBox.ItemsSource = typeof(Colors).GetProperties();
            dropDownColour1.ItemsSource = typeof(Colors).GetProperties();
            dropDownColour2.ItemsSource = typeof(Colors).GetProperties();
            dropDownColour3.ItemsSource = typeof(Colors).GetProperties();
            controller.LoadColorsForGraph(dropDownColour1, dropDownColour2, dropDownColour3);
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

        private void KpiListVIew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem != null)
            {
                controller.CastSelectedListViewItem((sender as ListView).SelectedItem);

                controller.LoadListViewValuesToChangeable(TitleTextBox, UnitTextBox, ColorComboBox, DataComboBox, IsActiveCheckBox);
                controller.AdjustButtonsAfterSelection(SaveKpiButton, DeleteKpiButton, AddKpiButton);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            controller.clearGraph();
            controller.SaveButton(dropDownComp1, dropDownStartYear1, dropDownEndYear1, dropDownColour1);
            controller.SaveButton(dropDownComp2, dropDownStartYear2, dropDownEndYear2, dropDownColour2);
            controller.SaveButton(dropDownComp3, dropDownStartYear3, dropDownEndYear3, dropDownColour3);
        }

        private void AddKpiButton_Click(object sender, RoutedEventArgs e)
        {
            controller.AddKpiToDB(TitleTextBox.Text, UnitTextBox.Text, DataComboBox, ColorComboBox);
            controller.SetKpiListViewSource(KpiListVIew);
        }

        private void SaveKpiButton_Click(object sender, RoutedEventArgs e)
        {
            controller.UpdateKpiInDB(TitleTextBox.Text, UnitTextBox.Text, ColorComboBox, IsActiveCheckBox.IsChecked.ToString(), DataComboBox);
            controller.SetKpiListViewSource(KpiListVIew);
        }

        private void DeleteKpiButton_Click(object sender, RoutedEventArgs e)
        {
            controller.DeleteKpiFromDB();
            controller.SetKpiListViewSource(KpiListVIew);
        }
    }
}
