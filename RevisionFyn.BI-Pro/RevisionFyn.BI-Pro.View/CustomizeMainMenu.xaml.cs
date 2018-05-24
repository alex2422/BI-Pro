using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RevisionFyn.BI_Pro.Controller;

namespace RevisionFyn.BI_Pro.View
{
    public partial class CustomizeMainMenu : Page
    {
        CustomizeStartScreenController controller = CustomizeStartScreenController.GetInstance();

        public CustomizeMainMenu()
        {
            InitializeComponent();

            controller.SetKpiListViewSource(KpiListVIew);
            controller.LoadValuesIntoKpiDataComboBox(DataComboBox);
            controller.AdjustInitalButtons(SaveKpiButton, DeleteKpiButton, AddKpiButton);
            controller.LoadClientsToComboBox(Client1ComboBox, Client2ComboBox, Client3ComboBox);
            controller.LoadYearsToComboBox(Year1ComboBox, Year2ComboBox, Year3ComboBox);
            controller.LoadYearsToComboBox(EndYear1ComboBox, EndYear2ComboBox, EndYear3ComboBox);
            ColorComboBox.ItemsSource = typeof(Colors).GetProperties();
            Color1ComboBox.ItemsSource = typeof(Colors).GetProperties();
            Color2ComboBox.ItemsSource = typeof(Colors).GetProperties();
            Color3ComboBox.ItemsSource = typeof(Colors).GetProperties();
            controller.LoadColorsForGraph(Color1ComboBox, Color2ComboBox, Color3ComboBox);
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
            controller.ClearGraph();
            controller.SaveGraphButton(Client1ComboBox, Year1ComboBox, EndYear1ComboBox, Color1ComboBox);
            controller.SaveGraphButton(Client2ComboBox, Year2ComboBox, EndYear2ComboBox, Color2ComboBox);
            controller.SaveGraphButton(Client3ComboBox, Year3ComboBox, EndYear3ComboBox, Color3ComboBox);
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
