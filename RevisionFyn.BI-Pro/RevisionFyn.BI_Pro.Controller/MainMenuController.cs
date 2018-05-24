using System;
using System.Collections.Generic;
using System.Linq;
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
        private static MainMenuController _ControllerInstance { get; set; }
        private StoredProcedure _StoredProcedure { get; set; }
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
            if (_ControllerInstance == null)
            {
                _ControllerInstance = new MainMenuController();
            }

            return _ControllerInstance;
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

        public void CreateGraph(Grid GraphGrid, Label Client1Label, Label Client2Label, Label Client3Label)
        {
            List<GraphData> graphData = _StoredProcedure.GetGraphData();

            if (graphData.Count != 0)
            {
                Client1Label.Content = graphData[0].Client;
                Client1Label.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(graphData[0].Color);
                Client2Label.Content = graphData[1].Client;
                Client2Label.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(graphData[1].Color);
                Client3Label.Content = graphData[2].Client;
                Client3Label.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(graphData[2].Color);

                foreach (GraphData data in graphData)
                {
                    List<int> xCoordinates = new List<int>();
                    List<int> yCoordinates = new List<int>();

                    for (int i = data.StartYear; i <= data.LastYear; i++)
                    {
                        xCoordinates.Add(i);
                    }

                    List<Client> clients = _StoredProcedure.GetClient();
                    clients.RemoveAll(s => s.ClientName != data.Client);

                    List<AccountCard> accountCards = _StoredProcedure.Getbalance(clients[0].ClientID);
                    accountCards.RemoveAll(s => s.Year > xCoordinates.Last());
                    accountCards.RemoveAll(s => s.Year < xCoordinates[0]);

                    foreach (AccountCard accCard in accountCards)
                    {
                        yCoordinates.Add(accCard.Balance);
                    }

                    yCoordinates.ToArray<int>();
                    xCoordinates.ToArray<int>();

                    LineGraph lg = new LineGraph
                    {
                        Description = data.Client,
                        Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(data.Color),
                        StrokeThickness = 2,
                    };

                    lg.Plot(xCoordinates, yCoordinates);
                    GraphGrid.Children.Add(lg);
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
            List<double> valuesFromChoosenClients = new List<double>();
            List<int> mappedClientID = _StoredProcedure.GetClientMapByStatisticsFavoriteID(customStatisticsRelatedToKPI.ID);
            List<Client> tempClients = new List<Client>();

            foreach (int clientID in mappedClientID)
            {
                tempClients.Add(_StoredProcedure.GetClientByID(clientID));
            }

            customStatisticsRelatedToKPI.ChoosenClients = tempClients;

            foreach (Client client in customStatisticsRelatedToKPI.ChoosenClients)
            {
                switch (customStatisticsRelatedToKPI.ChoosenStatisticsCalculationID)
                {
                    case 1:
                        foreach (double totalHoursValue in _StoredProcedure.GetTotalHoursByClientID(client.ClientID))
                        {
                            valuesFromChoosenClients.Add(totalHoursValue);
                        }
                        break;
                    case 2:
                        foreach (double salesAmountValue in _StoredProcedure.GetSalesAmountByClientID(client.ClientID))
                        {
                            valuesFromChoosenClients.Add(salesAmountValue);
                        }
                        break;
                    case 3:
                        foreach (double totalConsumptionValue in _StoredProcedure.GetTotalConsumptionByClientID(client.ClientID))
                        {
                            valuesFromChoosenClients.Add(totalConsumptionValue);
                        }
                        break;
                    case 4:
                        foreach (double balanceValue in _StoredProcedure.GetNegativeBalanceByClientID(client.ClientID))
                        {
                            valuesFromChoosenClients.Add(balanceValue);
                        }
                        break;
                    case 5:
                        foreach (double balanceValue in _StoredProcedure.GetPositiveBalanceByClientID(client.ClientID))
                        {
                            valuesFromChoosenClients.Add(balanceValue);
                        }
                        break;
                    default:
                        break;
                }
            }

            return valuesFromChoosenClients;
        }
        #endregion
    }
}
