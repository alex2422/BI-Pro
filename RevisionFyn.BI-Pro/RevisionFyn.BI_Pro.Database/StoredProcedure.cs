using RevisionFyn.BI_Pro.Model;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RevisionFyn.BI_Pro.Database
{
    public class StoredProcedure
    {
        private string ConnectionString { get; set; }

        public StoredProcedure()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ealSqlServer"].ConnectionString;
        }

        #region Add
        public string AddKPI(string kpiTitle, string kpiUnit, string kpiColor, int colorIndex, int dataID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_AddSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@Title", kpiTitle));
                    sqlCmd.Parameters.Add(new SqlParameter("@StatisticsFavoriteID", dataID));
                    sqlCmd.Parameters.Add(new SqlParameter("@Unit", kpiUnit));
                    sqlCmd.Parameters.Add(new SqlParameter("@Color", kpiColor));
                    sqlCmd.Parameters.Add(new SqlParameter("@ColorIndex", colorIndex));
                    sqlCmd.ExecuteNonQuery();

                    return "Succes: KPI'en er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
        }

        public string AddStatisticsFavorite(string favoriteName, int statisticsTypeID, int statisticsCalculationID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_AddStatisticsFavorite", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@Name", favoriteName));
                    sqlCmd.Parameters.Add(new SqlParameter("@StatisticsTypeID", statisticsTypeID));
                    sqlCmd.Parameters.Add(new SqlParameter("@StatisticsCalculationID", statisticsCalculationID));
                    sqlCmd.ExecuteNonQuery();

                    return "Succes: Statistikken er nu tilføjet til favoriter";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
        }

        public string AddStatisticsFavoriteClientMap(int clientID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_AddStatisticsFavoriteClientMap", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ClientID", clientID));
                    sqlCmd.ExecuteNonQuery();

                    return "Succes: Mappningen er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
        }

        public string AddGraphData(Client client, int startYear, int lastYear, string color, int colorIndex)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_AddToGraphData", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@Client", client.ClientName));
                    sqlCmd.Parameters.Add(new SqlParameter("@StartYear", startYear));
                    sqlCmd.Parameters.Add(new SqlParameter("@LastYear", lastYear));
                    sqlCmd.Parameters.Add(new SqlParameter("@Color", color));
                    sqlCmd.Parameters.Add(new SqlParameter("@ColorIndex", colorIndex));
                    sqlCmd.ExecuteNonQuery();

                    return "Succes: Grafdataen er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
        }
        #endregion

        #region Update
        public string UpdateKPI(int kpiID, string kpiTitle, string kpiUnit, string kpiColor, int colorIndex, string isActive, int dataID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_UpdateSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ID", kpiID));
                    sqlCmd.Parameters.Add(new SqlParameter("@Title", kpiTitle));
                    sqlCmd.Parameters.Add(new SqlParameter("@StatisticsFavoriteID", dataID));
                    sqlCmd.Parameters.Add(new SqlParameter("@Unit", kpiUnit));
                    sqlCmd.Parameters.Add(new SqlParameter("@Color", kpiColor));
                    sqlCmd.Parameters.Add(new SqlParameter("@ColorIndex", colorIndex));
                    sqlCmd.Parameters.Add(new SqlParameter("@IsActiveInput", isActive));
                    sqlCmd.ExecuteNonQuery();

                    return "Succes: KPI'en er nu opdateret";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
        }
        #endregion

        #region Delete
        public string DeleteKPI(int kpiID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_DeleteSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ID", kpiID));
                    sqlCmd.ExecuteNonQuery();

                    return "Succes: KPI'en er nu slettet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
        }
        #endregion

        #region Get
        public List<KPI> GetKPI()
        {
            List<KPI> kpis = new List<KPI>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string kpiID = reader["KpiID"].ToString();
                            string kpiTitle = reader["Title"].ToString();
                            string dataID = reader["FK_StatisticsFavoriteID"].ToString();
                            string kpiUnit = reader["Unit"].ToString();
                            string kpiColor = reader["Color"].ToString();
                            string colorIndex = reader["ColorIndex"].ToString();
                            string isActive = reader["IsActive"].ToString();

                            Int32.TryParse(kpiID, out int convertedKpiID);
                            Int32.TryParse(colorIndex, out int convertedColorIndex);
                            Int32.TryParse(dataID, out int convertedDataID);

                            if (isActive == "True")
                            {
                                isActive = "Ja";
                            }
                            else
                            {
                                isActive = "Nej";
                            }

                            kpis.Add(new KPI()
                            {
                                ID = convertedKpiID,
                                Title = kpiTitle,
                                DataID = convertedDataID,
                                Unit = kpiUnit,
                                Color = kpiColor,
                                ColorIndex = convertedColorIndex,
                                IsActive = isActive
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return kpis;
        }

        public List<KPI> GetActiveKPI()
        {
            List<KPI> activeKPIs = new List<KPI>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetActiveSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string kpiID = reader["KpiID"].ToString();
                            string kpiTitle = reader["Title"].ToString();
                            string dataID = reader["FK_StatisticsFavoriteID"].ToString();
                            string kpiUnit = reader["Unit"].ToString();
                            string kpiColor = reader["Color"].ToString();
                            string colorIndex = reader["ColorIndex"].ToString();
                            string isActive = reader["IsActive"].ToString();

                            Int32.TryParse(kpiID, out int convertedKpiID);
                            Int32.TryParse(colorIndex, out int convertedColorIndex);
                            Int32.TryParse(dataID, out int convertedDataID);

                            if (isActive == "True")
                            {
                                isActive = "Ja";
                            }
                            else
                            {
                                isActive = "Nej";
                            }

                            activeKPIs.Add(new KPI()
                            {
                                ID = convertedKpiID,
                                Title = kpiTitle,
                                DataID = convertedDataID,
                                Unit = kpiUnit,
                                Color = kpiColor,
                                ColorIndex = convertedColorIndex,
                                IsActive = isActive
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return activeKPIs;
        }

        public List<StatisticsType> GetActiveStatisticsType()
        {
            List<StatisticsType> activeStatisticsTypes = new List<StatisticsType>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetStatisticsType", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string statisticsTypeID = reader["StatisticsTypeID"].ToString();
                            string typeName = reader["Name"].ToString();

                            Int32.TryParse(statisticsTypeID, out int convertedStatisticsTypeID);

                            activeStatisticsTypes.Add(new StatisticsType()
                            {
                                ID = convertedStatisticsTypeID,
                                Name = typeName,
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return activeStatisticsTypes;
        }

        public List<StatisticsCalculation> GetActiveStatisticsCalculation()
        {
            List<StatisticsCalculation> activeStatisticsCalculations = new List<StatisticsCalculation>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetActiveStatisticsCalculation", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string statisticsCalculationID = reader["StatisticsCalculationID"].ToString();
                            string calculationName = reader["Name"].ToString();

                            Int32.TryParse(statisticsCalculationID, out int convertedStatisticsTypeID);

                            activeStatisticsCalculations.Add(new StatisticsCalculation()
                            {
                                ID = convertedStatisticsTypeID,
                                Name = calculationName
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return activeStatisticsCalculations;
        }

        public List<CustomStatistics> GetActiveStatisticsFavorite()
        {
            List<CustomStatistics> activeStatisticsFavorites = new List<CustomStatistics>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetActiveStatisticsFavorite", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string statisticsFavoriteID = reader["StatisticsFavoriteID"].ToString();
                            string name = reader["Name"].ToString();
                            string statisticsTypeID = reader["FK_StatisticsTypeID"].ToString();
                            string statisticsCalculationID = reader["FK_StatisticsCalculationID"].ToString();

                            Int32.TryParse(statisticsFavoriteID, out int convertedStatisticsFavoriteID);
                            Int32.TryParse(statisticsTypeID, out int convertedStatisticsTypeID);
                            Int32.TryParse(statisticsCalculationID, out int convertedStatisticsCalculationID);

                            activeStatisticsFavorites.Add(new CustomStatistics()
                            {
                                ID = convertedStatisticsFavoriteID,
                                Name = name,
                                ChoosenStatisticsTypeID = convertedStatisticsTypeID,
                                ChoosenStatisticsCalculationID = convertedStatisticsCalculationID
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return activeStatisticsFavorites;
        }

        public CustomStatistics GetStatisticsFavoriteByID(int requestedStatisticsFavoriteID)
        {
            CustomStatistics statisticsFavorites = new CustomStatistics();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetStatisticsFavoriteByID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@StatisticsFavoriteID", requestedStatisticsFavoriteID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string statisticsFavoriteID = reader["StatisticsFavoriteID"].ToString();
                            string name = reader["Name"].ToString();
                            string statisticsTypeID = reader["FK_StatisticsTypeID"].ToString();
                            string statisticsCalculationID = reader["FK_StatisticsCalculationID"].ToString();

                            Int32.TryParse(statisticsFavoriteID, out int convertedStatisticsFavoriteID);
                            Int32.TryParse(statisticsTypeID, out int convertedStatisticsTypeID);
                            Int32.TryParse(statisticsCalculationID, out int convertedStatisticsCalculationID);

                            statisticsFavorites = new CustomStatistics()
                            {
                                ID = convertedStatisticsFavoriteID,
                                Name = name,
                                ChoosenStatisticsTypeID = convertedStatisticsTypeID,
                                ChoosenStatisticsCalculationID = convertedStatisticsCalculationID
                            };
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return statisticsFavorites;
        }

        public List<int> GetClientMapByStatisticsFavoriteID(int requestedStatisticsFavoriteID)
        {
            List<int> clientMappings = new List<int>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetClientMapByStatisticsFavoriteID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@StatisticsFavoriteID", requestedStatisticsFavoriteID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clientID = reader["FK_ClientID"].ToString();

                            Int32.TryParse(clientID, out int convertedClientID);

                            clientMappings.Add(convertedClientID);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return clientMappings;
        }

        public Client GetClientByID(int requestedClientID)
        {
            Client client = new Client();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetClientByID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ClientID", requestedClientID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clientID = reader["ClientID"].ToString();
                            string clientName = reader["ClientName"].ToString();
                            string mainEmployee = reader["FK_MainEmployeeID"].ToString();
                            string startYear = reader["StartYear"].ToString();

                            Int32.TryParse(clientID, out int convertedClientID);

                            client = new Client()
                            {
                                ClientID = convertedClientID,
                                ClientName = clientName,
                                ClientStartYear = Convert.ToInt32(startYear),
                                AccountCards = Getbalance(convertedClientID),
                                MainEmployee = new Employee
                                {
                                    EmployeeID = Convert.ToInt32(mainEmployee),
                                }
                            };
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return client;
            }
        }

        public List<double> GetNegativeBalanceByClientID(int requestedClientID)
        {
            List<double> negativeBalanceValues = new List<double>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetNegativeBalanceByClientID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ClientID", requestedClientID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string balanceValue = reader["Balance"].ToString();

                            Double.TryParse(balanceValue, out double convertedBalanceValue);

                            negativeBalanceValues.Add(convertedBalanceValue);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return negativeBalanceValues;
        }

        public List<double> GetPositiveBalanceByClientID(int requestedClientID)
        {
            List<double> positiveBalanceValues = new List<double>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetPositiveBalanceByClientID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ClientID", requestedClientID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string balanceValue = reader["Balance"].ToString();

                            Double.TryParse(balanceValue, out double convertedBalanceValue);

                            positiveBalanceValues.Add(convertedBalanceValue);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return positiveBalanceValues;
        }

        public List<double> GetTotalHoursByClientID(int requestedClientID)
        {
            List<double> totalHourValues = new List<double>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetTotalHoursByClientID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ClientID", requestedClientID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string totalHoursValue = reader["TotalHours"].ToString();

                            Double.TryParse(totalHoursValue, out double convertedTotalHoursValue);

                            totalHourValues.Add(convertedTotalHoursValue);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return totalHourValues;
        }

        public List<double> GetSalesAmountByClientID(int requestedClientID)
        {
            List<double> salesAmountValues = new List<double>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetSalesAmountByClientID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ClientID", requestedClientID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string salesAmountValue = reader["SalesAmount"].ToString();

                            Double.TryParse(salesAmountValue, out double convertedSalesAmountValue);

                            salesAmountValues.Add(convertedSalesAmountValue);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return salesAmountValues;
        }

        public List<double> GetTotalConsumptionByClientID(int requestedClientID)
        {
            List<double> totalConsumptionValues = new List<double>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetTotalConsumptionByClientID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.Add(new SqlParameter("@ClientID", requestedClientID));

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string totalConsumptionValue = reader["TotalConsumption"].ToString();

                            Double.TryParse(totalConsumptionValue, out double convertedTotalConsumptionValue);

                            totalConsumptionValues.Add(convertedTotalConsumptionValue);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return totalConsumptionValues;
        }

        public List<Client> GetClient()
        {
            List<Client> clients = new List<Client>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetClient", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clientID = reader["ClientID"].ToString();
                            string clientName = reader["ClientName"].ToString();
                            string mainEmployee = reader["FK_MainEmployeeID"].ToString();
                            string startYear = reader["StartYear"].ToString();

                            Int32.TryParse(clientID, out int convertedClientID);

                            clients.Add(new Client()
                            {
                                ClientID = convertedClientID,
                                ClientName = clientName,
                                ClientStartYear = Convert.ToInt32(startYear),
                                AccountCards = Getbalance(convertedClientID),
                                MainEmployee = new Employee
                                {
                                    EmployeeID = Convert.ToInt32(mainEmployee),
                                }
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return clients;
            }
        }

        public List<Employee> GetEmployee()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetEmployee", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string employeeID = reader["EmployeeID"].ToString();
                            string employeeName = reader["EmployeeFirstName"].ToString();
                            string employeeLastName = reader["EmployeeLastName"].ToString();
                            string position = reader["EmployeePosition"].ToString();

                            Int32.TryParse(employeeID, out int convertedEmployeeID);

                            employees.Add(new Employee()
                            {
                                EmployeeID = convertedEmployeeID,
                                FirstName = employeeName,
                                LastName = employeeLastName,
                                Position = position
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return employees;
            }
        }

        public List<AccountCard> Getbalance(int requestedClientID)
        {
            List<AccountCard> balances = new List<AccountCard>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetBalance", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.Parameters.AddWithValue("@ClientID", requestedClientID);

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string balance = reader["Balance"].ToString();
                            string year = reader["Year"].ToString();
                            string clientID = reader["FK_ClientID"].ToString();

                            Int32.TryParse(balance, out int convertedBalance);
                            Int32.TryParse(year, out int convertedYear);
                            Int32.TryParse(clientID, out int convertedClientID);

                            balances.Add(new AccountCard()
                            {
                                Balance = convertedBalance,
                                Year = convertedYear,
                                ClientID = convertedClientID
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                balances.Sort((x, y) => x.Year.CompareTo(y.Year));

                return balances;
            }
        }

        public List<GraphData> GetGraphData()
        {
            List<GraphData> graphData = new List<GraphData>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetGraphData", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clientName = reader["Client"].ToString();
                            string startYear = reader["StartYear"].ToString();
                            string lastYear = reader["LastYear"].ToString();
                            string color = reader["Color"].ToString();
                            string colorIndex = reader["ColorIndex"].ToString();

                            Int32.TryParse(startYear, out int convertedStartYear);
                            Int32.TryParse(lastYear, out int convertedLastYear);
                            Int32.TryParse(colorIndex, out int convertedColorIndex);

                            graphData.Add(new GraphData()
                            {
                                Client = clientName,
                                StartYear = convertedStartYear,
                                LastYear = convertedLastYear,
                                Color = color,
                                ColorIndex = convertedColorIndex
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return graphData;
            }
        }

        public List<AccountCard> GetYear()
        {
            List<AccountCard> years = new List<AccountCard>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_GetYear", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string year = reader["Year"].ToString();

                            Int32.TryParse(year, out int convertedYear);

                            years.Add(new AccountCard()
                            {
                                Year = convertedYear
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return years;
            }
        }
        #endregion

        #region Count
        public int CountActiveKPI()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_CountActiveSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string activeKPI = reader["ActiveKPI"].ToString();

                            Int32.TryParse(activeKPI, out int convertedActiveKPI);

                            return convertedActiveKPI;
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return -1;
        }
        #endregion

        #region Clear
        public string ClearGraphData()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand sqlCmd = new SqlCommand("sp_ClearGraphdata", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlCmd.ExecuteNonQuery();

                    return "Succes: graphData er nu cleared";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
        }
        #endregion
    }
}