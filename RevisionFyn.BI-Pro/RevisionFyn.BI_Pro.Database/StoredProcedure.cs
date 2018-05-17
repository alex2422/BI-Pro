using RevisionFyn.BI_Pro.Model;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace RevisionFyn.BI_Pro.Database
{
    public class StoredProcedure
    {
        private string ConnectionString { get; set; }

        public StoredProcedure()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ealSqlServer"].ConnectionString;
        }

        #region Stored procedures - KPI

        public string AddKPI(string kpiTitle, string kpiUnit, string kpiColor, int colorIndex, int dataID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand addKpiCmd = new SqlCommand("sp_AddSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addKpiCmd.Parameters.Add(new SqlParameter("@Title", kpiTitle));
                    addKpiCmd.Parameters.Add(new SqlParameter("@StatisticsFavoriteID", dataID));
                    addKpiCmd.Parameters.Add(new SqlParameter("@Unit", kpiUnit));
                    addKpiCmd.Parameters.Add(new SqlParameter("@Color", kpiColor));
                    addKpiCmd.Parameters.Add(new SqlParameter("@ColorIndex", colorIndex));
                    addKpiCmd.ExecuteNonQuery();

                    result = "Succes: KPI'en er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }

            return result;
        }

        public string UpdateKPI(int kpiID, string kpiTitle, string kpiUnit, string kpiColor, int colorIndex, string isActive, int dataID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand updateKpiCmd = new SqlCommand("sp_UpdateSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    updateKpiCmd.Parameters.Add(new SqlParameter("@ID", kpiID));
                    updateKpiCmd.Parameters.Add(new SqlParameter("@Title", kpiTitle));
                    updateKpiCmd.Parameters.Add(new SqlParameter("@StatisticsFavoriteID", dataID));
                    updateKpiCmd.Parameters.Add(new SqlParameter("@Unit", kpiUnit));
                    updateKpiCmd.Parameters.Add(new SqlParameter("@Color", kpiColor));
                    updateKpiCmd.Parameters.Add(new SqlParameter("@ColorIndex", colorIndex));
                    updateKpiCmd.Parameters.Add(new SqlParameter("@IsActiveInput", isActive));
                    updateKpiCmd.ExecuteNonQuery();

                    result = "Succes: KPI'en er nu opdateret";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }

            return result;
        }

        public string DeleteKPI(int kpiID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand deleteKpiCmd = new SqlCommand("sp_DeleteSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    deleteKpiCmd.Parameters.Add(new SqlParameter("@ID", kpiID));
                    deleteKpiCmd.ExecuteNonQuery();

                    result = "Succes: KPI'en er nu slettet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }

            return result;
        }

        public int CountActiveKPI()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand countActiveKpiCmd = new SqlCommand("sp_CountActiveSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = countActiveKpiCmd.ExecuteReader();

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

        public List<KPI> GetKPI()
        {
            List<KPI> result = new List<KPI>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getKpiCmd = new SqlCommand("sp_GetSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = getKpiCmd.ExecuteReader();

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

                            result.Add(new KPI()
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
            return result;
        }

        public List<KPI> GetActiveKPI()
        {
            List<KPI> result = new List<KPI>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getKpiCmd = new SqlCommand("sp_GetActiveSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = getKpiCmd.ExecuteReader();

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

                            result.Add(new KPI()
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
            return result;
        }
        #endregion

        #region Stored procedures - Statistics
        public List<StatisticsType> GetActiveStatisticsType()
        {
            List<StatisticsType> result = new List<StatisticsType>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getActiveStatisticsTypeCmd = new SqlCommand("sp_GetStatisticsType", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = getActiveStatisticsTypeCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string statisticsTypeID = reader["StatisticsTypeID"].ToString();
                            string typeName = reader["Name"].ToString();

                            Int32.TryParse(statisticsTypeID, out int convertedStatisticsTypeID);

                            result.Add(new StatisticsType()
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
            return result;
        }
        public List<StatisticsCalculation> GetActiveStatisticsCalculation()
        {
            List<StatisticsCalculation> result = new List<StatisticsCalculation>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getActiveStatisticsCalculationCmd = new SqlCommand("sp_GetActiveStatisticsCalculation", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = getActiveStatisticsCalculationCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string statisticsCalculationID = reader["StatisticsCalculationID"].ToString();
                            string calculationName = reader["Name"].ToString();

                            Int32.TryParse(statisticsCalculationID, out int convertedStatisticsTypeID);

                            result.Add(new StatisticsCalculation()
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
            return result;
        }
        public string AddStatisticsFavorite(string favoriteName, int statisticsTypeID, int statisticsCalculationID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand addStatisticsFavoriteCmd = new SqlCommand("sp_AddStatisticsFavorite", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addStatisticsFavoriteCmd.Parameters.Add(new SqlParameter("@Name", favoriteName));
                    addStatisticsFavoriteCmd.Parameters.Add(new SqlParameter("@StatisticsTypeID", statisticsTypeID));
                    addStatisticsFavoriteCmd.Parameters.Add(new SqlParameter("@StatisticsCalculationID", statisticsCalculationID));
                    addStatisticsFavoriteCmd.ExecuteNonQuery();

                    result = "Succes: Statistikken er nu tilføjet til favoriter";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }

            return result;
        }

        public string AddStatisticsFavoriteClientMap(int clientID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand addStatisticsFavoriteClientMapCmd = new SqlCommand("sp_AddStatisticsFavoriteClientMap", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addStatisticsFavoriteClientMapCmd.Parameters.Add(new SqlParameter("@ClientID", clientID));
                    addStatisticsFavoriteClientMapCmd.ExecuteNonQuery();

                    result = "Succes: Mappningen er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }

            return result;
        }

        public List<CustomStatistics> GetActiveStatisticsFavorite()
        {
            List<CustomStatistics> result = new List<CustomStatistics>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getActiveStatisticsTypeCmd = new SqlCommand("sp_GetActiveStatisticsFavorite", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = getActiveStatisticsTypeCmd.ExecuteReader();

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

                            result.Add(new CustomStatistics()
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
            return result;
        }

        public CustomStatistics GetStatisticsFavoriteByID(int requestedStatisticsFavoriteID)
        {
            CustomStatistics result = new CustomStatistics();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getActiveStatisticsTypeCmd = new SqlCommand("sp_GetStatisticsFavoriteByID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    getActiveStatisticsTypeCmd.Parameters.Add(new SqlParameter("@StatisticsFavoriteID", requestedStatisticsFavoriteID));

                    SqlDataReader reader = getActiveStatisticsTypeCmd.ExecuteReader();

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

                            result = new CustomStatistics()
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
            return result;
        }

        public List<int> GetClientMapByStatisticsFavoriteID (int requestedStatisticsFavoriteID)
        {
            List<int> result = new List<int>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getActiveStatisticsTypeCmd = new SqlCommand("sp_GetClientMapByStatisticsFavoriteID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    getActiveStatisticsTypeCmd.Parameters.Add(new SqlParameter("@StatisticsFavoriteID", requestedStatisticsFavoriteID));

                    SqlDataReader reader = getActiveStatisticsTypeCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clientID = reader["FK_ClientID"].ToString();

                            Int32.TryParse(clientID, out int convertedClientID);

                            result.Add(convertedClientID);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }

        public Company GetCompaniesByID(int requestedClientID)
        {
            Company company = new Company();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getClient = new SqlCommand("sp_GetClientByID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    getClient.Parameters.Add(new SqlParameter("@ClientID", requestedClientID));

                    SqlDataReader reader = getClient.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clientID = reader["ClientID"].ToString();
                            string clientName = reader["ClientName"].ToString();
                            string mainEmployee = reader["FK_MainEmployeeID"].ToString();
                            string startYear = reader["StartYear"].ToString();

                            Int32.TryParse(clientID, out int convertedClientID);


                            company = new Company()
                            {
                                CompanyID = convertedClientID,
                                CompanyName = clientName,
                                CompanyStartYear = Convert.ToInt32(startYear),
                                accountCards = Getbalance(convertedClientID),
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
                return company;
            }
        }

        public List<double> GetNegativeBalanceByClientID(int requestedClientID)
        {
            List<double> result = new List<double>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getActiveStatisticsTypeCmd = new SqlCommand("sp_GetNegativeBalanceByClientID", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    getActiveStatisticsTypeCmd.Parameters.Add(new SqlParameter("@ClientID", requestedClientID));

                    SqlDataReader reader = getActiveStatisticsTypeCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string balanceValue = reader["Balance"].ToString();

                            Double.TryParse(balanceValue, out double convertedBalanceValue);

                            result.Add(convertedBalanceValue);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }
        #endregion

        public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getClient = new SqlCommand("sp_GetClient", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = getClient.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clientID = reader["ClientID"].ToString();
                            string clientName = reader["ClientName"].ToString();
                            string mainEmployee = reader["FK_MainEmployeeID"].ToString();
                            string startYear = reader["StartYear"].ToString();

                            Int32.TryParse(clientID, out int convertedClientID);


                            companies.Add(new Company()
                            {
                                CompanyID = convertedClientID,
                                CompanyName = clientName,
                                CompanyStartYear = Convert.ToInt32(startYear),
                                accountCards = Getbalance(convertedClientID),
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
                return companies;
            }
        }

        public List<Employee> getEmployee()
        {
            List<Employee> employee = new List<Employee>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getEmployee = new SqlCommand("sp_´GetEmployee", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader reader = getEmployee.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string employeeID = reader["EmployeeID"].ToString();
                            string employeeName = reader["EmployeeFirstName"].ToString();
                            Int32.TryParse(employeeID, out int convertedEmployeeID);

                            employee.Add(new Employee()
                            {
                                EmployeeID = convertedEmployeeID,
                                FirstName = employeeName,
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return employee;
            }
        }
        public List<AccountCard> Getbalance(int company)
        {
            List<AccountCard> listBalance = new List<AccountCard>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getBalance = new SqlCommand("sp_GetBalance", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    getBalance.Parameters.AddWithValue("@ClientID", company);
                    SqlDataReader reader = getBalance.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string balance = reader["Balance"].ToString();
                            int year = (int)reader["Year"];
                            int companyID = (int)reader["FK_ClientID"];

                            Int32.TryParse(balance, out int convertedBalance);

                            listBalance.Add(new AccountCard()
                            {
                                Balance = convertedBalance,
                                Year = year,
                                CompanyID = companyID
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                listBalance.Sort((x, y) => x.Year.CompareTo(y.Year)); ;
                return listBalance;
            }
        }
        public List<AccountCard> GetGraphData()
        {
            List<AccountCard> listBalance = new List<AccountCard>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getBalance = new SqlCommand("sp_GetGraphData", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader reader = getBalance.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string clientName = reader["ClientName"].ToString();
                            string balance = reader["Balance"].ToString();
                            int year = (int)reader["Year"];

                            Int32.TryParse(balance, out int convertedBalance);

                            listBalance.Add(new AccountCard()
                            {
                                CompanyName = clientName,
                                Balance = convertedBalance,
                                Year = year,
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                listBalance.Sort((x, y) => x.Year.CompareTo(y.Year)); ;
                return listBalance;
            }
        }

        public string AddClient(int clientID, string clientName, int startYear, int mainEmployeeID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand addClient = new SqlCommand("sp_AddClient", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addClient.Parameters.Add(new SqlParameter("@ClientID", clientID));
                    addClient.Parameters.Add(new SqlParameter("@ClientName", clientName));
                    addClient.Parameters.Add(new SqlParameter("@StartYear", startYear));
                    addClient.Parameters.Add(new SqlParameter("@MainEmployee", mainEmployeeID));
                    addClient.ExecuteNonQuery();

                    result = "Succes: Klienten er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
            return result;
        }
        public string AddEmployee(string firstName, string lastName, string position, int iD)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand addEmployee = new SqlCommand("sp_AddEmployee", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addEmployee.Parameters.Add(new SqlParameter("@EmployeeID", iD));
                    addEmployee.Parameters.Add(new SqlParameter("@EmployeePosition", position));
                    addEmployee.Parameters.Add(new SqlParameter("@EmployeeFirstName", firstName));
                    addEmployee.Parameters.Add(new SqlParameter("@EmployeeLastName", lastName));
                    addEmployee.ExecuteNonQuery();

                    result = "Succes: medarbejderen er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
            return result;
        }
        public string AddAccCard(string iD, int mainEmployeeID, int totalConsumption, int balance, int clientID, string clientName, string otherEmployeeIDs, double totalHours, int year, int numberOfTasks, int invoicePrice)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand addAccCard = new SqlCommand("sp_AddAccountCard", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addAccCard.Parameters.Add(new SqlParameter("@CaseID", iD));
                    addAccCard.Parameters.Add(new SqlParameter("@MainEmployeeID", mainEmployeeID));
                    addAccCard.Parameters.Add(new SqlParameter("@TotalConsumption", totalConsumption));
                    addAccCard.Parameters.Add(new SqlParameter("@Balance", balance));
                    addAccCard.Parameters.Add(new SqlParameter("@ClientID", clientID));
                    addAccCard.Parameters.Add(new SqlParameter("@ClientName", clientName));
                    addAccCard.Parameters.Add(new SqlParameter("@OtherEmployeeIDs", otherEmployeeIDs));
                    addAccCard.Parameters.Add(new SqlParameter("@TotalHours", totalHours));
                    addAccCard.Parameters.Add(new SqlParameter("@year", year));
                    addAccCard.Parameters.Add(new SqlParameter("@NumberOftasks", numberOfTasks));
                    addAccCard.Parameters.Add(new SqlParameter("@InvoicePrice", invoicePrice));
                    addAccCard.ExecuteNonQuery();

                    result = "Succes: kontokortet er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
            return result;
        }
        public List<AccountCard> GetYear()
        {
            List<AccountCard> listYear = new List<AccountCard>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand getYear = new SqlCommand("sp_GetYear", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader reader = getYear.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string year = reader["Year"].ToString();

                            Int32.TryParse(year, out int convertedYear);

                            listYear.Add(new AccountCard()
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
                return listYear;
            }
        }
    }
}
