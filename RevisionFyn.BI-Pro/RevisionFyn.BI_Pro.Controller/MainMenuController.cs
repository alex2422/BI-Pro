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
            // Get this from database later
            int numberOfKPI = 3;

            int space = 10;

            if (numberOfKPI != 0)
            {
                KpiGrid.Background = Brushes.White;
                KpiGrid.Children.RemoveAt(0);

                for (int i = 0; i < numberOfKPI; i++)
                {
                    Grid KpiContentGrid = new Grid
                    {
                        Name = "KpiContentGrid" + (i + 1),
                        Height = 160,
                        Width = 180,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(space, 0, 0, 0),
                        Background = Brushes.DodgerBlue
                    };

                    TextBlock KpiTitle = new TextBlock
                    {
                        Name = "KpiTitleTextBlock",
                        Text = "Total dækning (+/-)",
                        Foreground = Brushes.White,
                        Margin = new Thickness(10, 10, 0, 0),
                        FontSize = 17,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.WrapWithOverflow,
                    };

                    Label KpiValueLabel = new Label
                    {
                        Name = "KpiValueLabel",
                        Content = "304612",
                        Foreground = Brushes.White,
                        FontSize = 45,
                        Margin = new Thickness(4, 47, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left,
                    };

                    TextBlock KpiUnitTextBlock = new TextBlock
                    {
                        Name = "KpiUnitTextBlock",
                        Text = "DKK",
                        Foreground = Brushes.White,
                        Margin = new Thickness(10, 132, 0, 0),
                        FontSize = 15,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.WrapWithOverflow,
                    };

                    KpiContentGrid.Children.Add(KpiTitle);
                    KpiContentGrid.Children.Add(KpiValueLabel);
                    KpiContentGrid.Children.Add(KpiUnitTextBlock);
                    KpiGrid.Children.Add(KpiContentGrid);

                    space = space + 226;
                }
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
