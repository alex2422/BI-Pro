﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevisionFyn.BI_Pro.Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using InteractiveDataDisplay.WPF;

namespace RevisionFyn.BI_Pro.Controller
{
    public class MainMenuController
    {
        #region Variables
        private static MainMenuController controllerInstance;
        public List<Company> companies = new List<Company>();
        #endregion

        private MainMenuController()
        { }
        #region CreateMockData
        public void GenerateCompany()
        {
            for (int i = 0; i < 10; i++)
            {
                Company company = new Company();
                company.CompanyName = "Firma "+(i+1);
                company.CompanyID = i;
                Random randomNumberGenerator = new Random();
                int startYearGen = randomNumberGenerator.Next(2000, 2014);
                company.CompanyStartYear = startYearGen;
                DateTime thisDay = DateTime.Today;
                company.CompanyEndYear = Convert.ToInt32(thisDay.ToString("d").Split('/')[3])-1;
                company.MainEmployee = new Employee;
                for (int yeari = company.CompanyStartYear-1; yeari < company.CompanyEndYear; i++)
                {
                    company.years.Add(yeari);
                }
                foreach (var year in company.years)
                {
                    AccountCard accCard = new AccountCard();
                    accCard.CaseID = company.CompanyID+' '+-+' '+year;
                    accCard.MainEmployee = company.MainEmployee;
                    int otherEmps = randomNumberGenerator.Next(2, 5);
                    for (int i2 = 0; i2 < otherEmps; i2++)
                    {
                        Employee emp;
                        do
                        {
                            emp = new Employee;
                        } while (emp != accCard.MainEmployee);
                        accCard.otherEmployees.Add(emp);
                    }
                }
            }
        }
        #endregion
        #region Public Methods
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
                        Background = (SolidColorBrush)new BrushConverter().ConvertFromString(listOfKPI[i].Color),
                        ToolTip = listOfKPI[i].Title
                    };

                    TextBlock KpiTitle = new TextBlock
                    {
                        Name = "KpiTitleTextBlock",
                        Text = listOfKPI[i].Title,
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
        #endregion
    }
}
