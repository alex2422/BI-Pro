using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevisionFyn.BI_Pro.Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace RevisionFyn.BI_Pro.Controller
{
    public class MainMenuController
    {
        #region Variables
        private static MainMenuController controllerInstance;
        #endregion

        private MainMenuController()
        { }

        #region Public Methods
        public static MainMenuController GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new MainMenuController();
            }

            return controllerInstance;
        }

        public void CreateKpiElements(Grid KpiGrid)
        {
#if DEBUG
            KPI KPI_1 = new KPI
            {
                Title = "Total dækning (+/-)",
                Value = 304612,
                Unit = "DKK",
                Color = "Dodgerblue"
            };
            KPI KPI_2 = new KPI
            {
                Title = "Kunder med underdækning",
                Value = 42,
                Unit = "Antal",
                Color = "Red"
            };
            KPI KPI_3 = new KPI
            {
                Title = "Gns. dækning (+/-) pr. kunde",
                Value = 5840,
                Unit = "DKK",
                Color = "Black"
            };

            List<KPI> listOfKPI = new List<KPI>
            {
                KPI_1,
                KPI_2,
                KPI_3
            };
#endif

            // Get this from database later
            int numberOfKPI = 3;

            int kpiDisplacement = 10;

            if (numberOfKPI != 0)
            {
                HideDefaultKpiGrid(KpiGrid);

                for (int i = 0; i < numberOfKPI; i++)
                {
                    Grid KpiContentGrid = new Grid
                    {
                        Name = "KpiContentGrid" + (i + 1),
                        Height = 160,
                        Width = 180,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(kpiDisplacement, 0, 0, 0),
                        Background = (SolidColorBrush)new BrushConverter().ConvertFromString(listOfKPI[i].Color)
                    };

                    TextBlock KpiTitle = new TextBlock
                    {
                        Name = "KpiTitleTextBlock",
                        Text = listOfKPI[i].Title,
                        Foreground = Brushes.White,
                        Margin = new Thickness(10, 10, 0, 0),
                        FontSize = 17,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.Wrap,
                    };

                    Label KpiValueLabel = new Label
                    {
                        Name = "KpiValueLabel",
                        Content = listOfKPI[i].Value.ToString(),
                        Foreground = Brushes.White,
                        FontSize = 45,
                        Margin = new Thickness(4, 47, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left,
                    };

                    TextBlock KpiUnitTextBlock = new TextBlock
                    {
                        Name = "KpiUnitTextBlock",
                        Text = listOfKPI[i].Unit,
                        Foreground = Brushes.White,
                        Margin = new Thickness(10, 132, 0, 0),
                        FontSize = 15,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.Wrap,
                    };

                    KpiContentGrid.Children.Add(KpiTitle);
                    KpiContentGrid.Children.Add(KpiValueLabel);
                    KpiContentGrid.Children.Add(KpiUnitTextBlock);
                    KpiGrid.Children.Add(KpiContentGrid);

                    kpiDisplacement = kpiDisplacement + 226;
                }
            }
        }
        public void ManageGraph()
        {

        }
        #endregion

        #region Private Methods
        private void HideDefaultKpiGrid(Grid KpiGrid)
        {
            KpiGrid.Background = Brushes.White;
            KpiGrid.Children.RemoveAt(0);
        }
        #endregion
    }
}
