using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevisionFyn.BI_Pro.Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using InteractiveDataDisplay.WPF;
using RevisionFyn.BI_Pro.Database;

namespace RevisionFyn.BI_Pro.Controller
{
    public class MainMenuController
    {
        #region Variables / Properties
        private static MainMenuController controllerInstance;
        private StoredProcedure _StoredProcedure { get; set; }
        public List<Company> companies = new List<Company>();
        List<double> hourValues = new List<double>() { 0.25, 0.50, 0.75, 1.00, 1.25, 1.50, 1.75, 2.00, 2.25, 2.50, 2.75 };
        List<Company> listOfCompanies = new List<Company>();
        #endregion

        #region Constructor
        private MainMenuController()
        {
            _StoredProcedure = new StoredProcedure();
        }
        #endregion

        #region Public Methods
        public static MainMenuController GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new MainMenuController();
            }

            return controllerInstance;
        }

        public void CreateGraph(Grid graphGrid)
        {
            CustomizeStartScreenController cssc = CustomizeStartScreenController.GetInstance();
            companies=cssc.companies;
            if (companies.Count <= 0)
            {
                companies = cssc.companies;
            }
            foreach (var company in companies)
            {
                LineGraph lg = new LineGraph();
                graphGrid.Children.Add(lg);
                lg.Description = company.CompanyName;
                lg.Stroke = Brushes.Red;
                lg.StrokeThickness = 2;
                lg.Plot(company.x, company.y);
            }
        }

        public void CreateKpiElements(Grid KpiGrid)
        {
            #region Test data - get from database
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
            #endregion


            List<KPI> activeKPI = _StoredProcedure.GetActiveKPI();

            int numberOfActiveKPI = activeKPI.Count;
            int kpiDisplacement = 10;

            if (numberOfActiveKPI != 0)
            {
                HideDefaultKpiGrid(KpiGrid);

                for (int i = 0; i < numberOfActiveKPI; i++)
                {
                    CustomStatistics customStatisticsRelatedToKPI = _StoredProcedure.GetStatisticsFavoriteByID(activeKPI[i].DataID);

                    // TO DO: set double value based on selected companies
                    List<double> kpiRawValue = GetValueBasedOnCalculationSelection(customStatisticsRelatedToKPI);

                    // TO DO: set KPI value based on double value and statistics type
                    //activeKPI[i].Value = GetValueBasedOnTypeSelection(kpiRawValue, customStatisticsRelatedToKPI.ChoosenStatisticsTypeID);

                    Grid KpiContentGrid = new Grid
                    {
                        Name = "KpiContentGrid" + (i + 1),
                        Height = 160,
                        Width = 180,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(kpiDisplacement, 0, 0, 0),
                        Background = (SolidColorBrush)new BrushConverter().ConvertFromString(activeKPI[i].Color),
                        ToolTip = activeKPI[i].Title
                    };

                    TextBlock KpiTitle = new TextBlock
                    {
                        Name = "KpiTitleTextBlock",
                        Text = activeKPI[i].Title,
                        Foreground = Brushes.White,
                        Margin = new Thickness(10, 10, 0, 0),
                        FontSize = 17,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.NoWrap,
                        TextTrimming = TextTrimming.CharacterEllipsis
                    };

                    Label KpiValueLabel = new Label
                    {
                        Name = "KpiValueLabel",
                        Content = activeKPI[i].Value.ToString(),
                        Foreground = Brushes.White,
                        FontSize = 45,
                        Margin = new Thickness(4, 47, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left,
                    };

                    TextBlock KpiUnitTextBlock = new TextBlock
                    {
                        Name = "KpiUnitTextBlock",
                        Text = activeKPI[i].Unit,
                        Foreground = Brushes.White,
                        Margin = new Thickness(10, 132, 0, 0),
                        FontSize = 15,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.NoWrap,
                        TextTrimming = TextTrimming.CharacterEllipsis
                    };

                    KpiContentGrid.Children.Add(KpiTitle);
                    KpiContentGrid.Children.Add(KpiValueLabel);
                    KpiContentGrid.Children.Add(KpiUnitTextBlock);
                    KpiGrid.Children.Add(KpiContentGrid);

                    kpiDisplacement = kpiDisplacement + 226;
                }
            }
        }
        #endregion

        #region Private Methods
        private void HideDefaultKpiGrid(Grid KpiGrid)
        {
            KpiGrid.Background = Brushes.White;
            KpiGrid.Children.RemoveAt(0);
        }

        private double GetValueBasedOnTypeSelection(double kpiRawValue, int statisticsTypeID)
        {
            throw new NotImplementedException();
        }

        private List<double> GetValueBasedOnCalculationSelection(CustomStatistics customStatisticsRelatedToKPI)
        {
            List<double> valuesFromChoosenCompanies = new List<double>();
            List<int> mappedClientID = _StoredProcedure.GetClientMapByStatisticsFavoriteID(customStatisticsRelatedToKPI.ID);
            List<Company> tempCompanies = new List<Company>();

            foreach (int clientID in mappedClientID)
            {
                tempCompanies.Add(_StoredProcedure.GetCompaniesByID(clientID));
            }

            customStatisticsRelatedToKPI.ChoosenCompanies = tempCompanies;

            foreach (Company company in customStatisticsRelatedToKPI.ChoosenCompanies)
            {
                switch (customStatisticsRelatedToKPI.ChoosenStatisticsCalculationID)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            }

            return valuesFromChoosenCompanies;
        }
        #endregion
    }
}
