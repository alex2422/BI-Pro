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
        public List<Client> companies = new List<Client>();
        List<double> hourValues = new List<double>() { 0.25, 0.50, 0.75, 1.00, 1.25, 1.50, 1.75, 2.00, 2.25, 2.50, 2.75 };
        List<Client> listOfCompanies = new List<Client>();
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

        public void CreateKpiElements(Grid KpiGrid)
        {
            List<KPI> activeKPI = _StoredProcedure.GetActiveKPI();

            int numberOfActiveKPI = activeKPI.Count;
            int kpiDisplacement = 15;

            if (numberOfActiveKPI != 0)
            {
                HideDefaultKpiGrid(KpiGrid);

                for (int i = 0; i < numberOfActiveKPI; i++)
                {
                    CustomStatistics customStatisticsRelatedToKPI = _StoredProcedure.GetStatisticsFavoriteByID(activeKPI[i].DataID);
                    List<double> kpiRawValue = GetValueBasedOnCalculationSelection(customStatisticsRelatedToKPI);

                    activeKPI[i].Value = GetValueBasedOnTypeSelection(kpiRawValue, customStatisticsRelatedToKPI.ChoosenStatisticsTypeID);

                    Grid KpiContentGrid = new Grid
                    {
                        Name = "KpiContentGrid" + (i + 1),
                        Height = 160,
                        Width = 200,
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
                        Content = String.Format("{0:0,0.##}", activeKPI[i].Value),
                        Foreground = Brushes.White,
                        FontSize = 40,
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
        public void createGraph(Grid graphGrid, Label label1, Label label2, Label label3)
        {
            List<GraphData> listOfGraphData = _StoredProcedure.GetGraphData();
            if (listOfGraphData.Count != 0)
            {
                label1.Content = listOfGraphData[0].Company;
                label1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(listOfGraphData[0].Color);
                label2.Content = listOfGraphData[1].Company;
                label2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(listOfGraphData[1].Color);
                label3.Content = listOfGraphData[2].Company;
                label3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(listOfGraphData[2].Color);
                foreach (GraphData graphData in listOfGraphData)
                {
                    List<int> xList = new List<int>();
                    List<int> yList = new List<int>();
                    List<AccountCard> accCardList = new List<AccountCard>();
                    List<Client> companyList = new List<Client>();
                    for (int i = graphData.StartYear; i <= graphData.LastYear; i++)
                    {
                        xList.Add(i);
                    }
                    companyList = _StoredProcedure.GetCompanies();
                    companyList.RemoveAll(s => s.ClientName != graphData.Company);
                    accCardList = _StoredProcedure.Getbalance(companyList[0].ClientID);
                    accCardList.RemoveAll(s => s.Year > xList.Last());
                    accCardList.RemoveAll(s => s.Year < xList[0]);
                    foreach (AccountCard accCard in accCardList)
                    {
                        yList.Add(accCard.Balance);
                    }
                    yList.ToArray<int>();
                    xList.ToArray<int>();
                    LineGraph lg = new LineGraph();
                    graphGrid.Children.Add(lg);
                    lg.Description = graphData.Company;
                    lg.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(graphData.Color);
                    lg.StrokeThickness = 2;
                    lg.Plot(xList, yList);
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

        private double GetValueBasedOnTypeSelection(List<double> kpiRawValue, int statisticsTypeID)
        {
            switch (statisticsTypeID)
            {
                case 1:
                    return CustomStatistics.Average(kpiRawValue);
                case 2:
                    return CustomStatistics.Highest(kpiRawValue);
                case 3:
                    return CustomStatistics.Lowest(kpiRawValue);
                case 4:
                    return CustomStatistics.Count(kpiRawValue);
                case 5:
                    return CustomStatistics.Sum(kpiRawValue);
                default:
                    return 0;
            }
        }

        private List<double> GetValueBasedOnCalculationSelection(CustomStatistics customStatisticsRelatedToKPI)
        {
            List<double> valuesFromChoosenCompanies = new List<double>();
            List<int> mappedClientID = _StoredProcedure.GetClientMapByStatisticsFavoriteID(customStatisticsRelatedToKPI.ID);
            List<Client> tempCompanies = new List<Client>();

            foreach (int clientID in mappedClientID)
            {
                tempCompanies.Add(_StoredProcedure.GetCompaniesByID(clientID));
            }

            customStatisticsRelatedToKPI.ChoosenCompanies = tempCompanies;

            foreach (Client company in customStatisticsRelatedToKPI.ChoosenCompanies)
            {
                switch (customStatisticsRelatedToKPI.ChoosenStatisticsCalculationID)
                {
                    case 1:
                        foreach (double totalHoursValue in _StoredProcedure.GetTotalHoursByClientID(company.ClientID))
                        {
                            valuesFromChoosenCompanies.Add(totalHoursValue);
                        }
                        break;
                    case 2:
                        foreach (double salesAmountValue in _StoredProcedure.GetSalesAmountByClientID(company.ClientID))
                        {
                            valuesFromChoosenCompanies.Add(salesAmountValue);
                        }
                        break;
                    case 3:
                        foreach (double totalConsumptionValue in _StoredProcedure.GetTotalConsumptionByClientID(company.ClientID))
                        {
                            valuesFromChoosenCompanies.Add(totalConsumptionValue);
                        }
                        break;
                    case 4:
                        foreach (double balanceValue in _StoredProcedure.GetNegativeBalanceByClientID(company.ClientID))
                        {
                            valuesFromChoosenCompanies.Add(balanceValue);
                        }
                        break;
                    case 5:
                        foreach (double balanceValue in _StoredProcedure.GetPositiveBalanceByClientID(company.ClientID))
                        {
                            valuesFromChoosenCompanies.Add(balanceValue);
                        }
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
