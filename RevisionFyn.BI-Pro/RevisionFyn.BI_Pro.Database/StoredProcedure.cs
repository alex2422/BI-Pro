using RevisionFyn.BI_Pro.Model;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace RevisionFyn.BI_Pro.Database
{
    public class StoredProcedure
    {
        private static string connectionString = "Server = EALSQL1.eal.local; Database = DB2017_C07; User Id = USER_C07; Password = SesamLukOp_07";

        #region Stored procedures - KPI

        public string AddKPI(string kpiTitle, string kpiUnit, string kpiColor, int colorIndex)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand addKpiCmd = new SqlCommand("sp_AddSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addKpiCmd.Parameters.Add(new SqlParameter("@Title", kpiTitle));
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

        public string UpdateKPI(int kpiID, string kpiTitle, string kpiUnit, string kpiColor, int colorIndex, string isActive)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(connectionString))
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

            using (SqlConnection con = new SqlConnection(connectionString))
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
            using (SqlConnection con = new SqlConnection(connectionString))
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

            using (SqlConnection con = new SqlConnection(connectionString))
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
                            string kpiUnit = reader["Unit"].ToString();
                            string kpiColor = reader["Color"].ToString();
                            string colorIndex = reader["ColorIndex"].ToString();
                            string isActive = reader["IsActive"].ToString();

                            Int32.TryParse(kpiID, out int convertedKpiID);
                            Int32.TryParse(colorIndex, out int convertedColorIndex);

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

            using (SqlConnection con = new SqlConnection(connectionString))
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
                            string typeExternalSource = reader["ExternalSource"].ToString();

                            Int32.TryParse(statisticsTypeID, out int convertedStatisticsTypeID);

                            result.Add(new StatisticsType()
                            {
                                ID = convertedStatisticsTypeID,
                                Name = typeName,
                                ExternalSource = typeExternalSource
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

            using (SqlConnection con = new SqlConnection(connectionString))
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
        #endregion

        public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();

            using (SqlConnection con = new SqlConnection(connectionString))
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
                                //CompanyStartYear = Convert.ToInt32(startYear),
                                //MainEmployee = new Employee
                                //{
                                //    EmployeeID = Convert.ToInt32(mainEmployee),
                                //}
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

            using (SqlConnection con = new SqlConnection(connectionString))
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

                            Int32.TryParse(employeeID, out int convertedEmployeeID);

                            employee.Add(new Employee()
                            {
                                EmployeeID = convertedEmployeeID,
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
        public List<AccountCard> Getbalance()
        {
            List<AccountCard> listBalance = new List<AccountCard>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getBalance = new SqlCommand("sp_GetBalance", con)
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

                            Int32.TryParse(balance, out int convertedBalance);

                            listBalance.Add(new AccountCard()
                            {
                                CompanyName = clientName,
                                Balance = convertedBalance,
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return listBalance;
            }
        }
        public string AddClient(int clientID, string clientName, int startYear, Employee mainEmployee)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(connectionString))
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
                    addClient.Parameters.Add(new SqlParameter("@MainEmployee", mainEmployee));

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

            using (SqlConnection con = new SqlConnection(connectionString))
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

            using (SqlConnection con = new SqlConnection(connectionString))
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

                    result = "Succes: kontokortet er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }
            return result;
        }
    }
}
