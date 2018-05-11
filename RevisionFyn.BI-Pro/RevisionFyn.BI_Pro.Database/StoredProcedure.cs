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
                            string mainEmployee = reader["MainEmployee"].ToString();
                            string startYear = reader["SartYear"].ToString();

                            Int32.TryParse(clientID, out int convertedClientID);
                            

                            companies.Add(new Company()
                            {
                                CompanyID = convertedClientID,
                                CompanyName = clientName,
                                CompanyStartYear = Convert.ToInt32(startYear),
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
            List<Employee> Employee = new List<Employee>();

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

                            Employee.Add(new Employee()
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
                return Employee;
            }
        }
        #endregion
    }
}
