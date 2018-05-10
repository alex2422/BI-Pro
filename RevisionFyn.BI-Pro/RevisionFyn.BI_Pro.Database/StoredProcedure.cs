using RevisionFyn.BI_Pro.Model;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Database
{
    public class StoredProcedure
    {
        private static string connectionString = "Server = EALSQL1.eal.local; Database = DB2017_C07; User Id = USER_C07; Password = SesamLukOp_07";

        public string AddSystemKPI(string kpiTitle, string kpiUnit, string kpiColor, int colorIndex, string isActive)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand addSystemKpiCmd = new SqlCommand("sp_AddSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addSystemKpiCmd.Parameters.Add(new SqlParameter("@Title", kpiTitle));
                    addSystemKpiCmd.Parameters.Add(new SqlParameter("@Unit", kpiUnit));
                    addSystemKpiCmd.Parameters.Add(new SqlParameter("@Color", kpiColor));
                    addSystemKpiCmd.Parameters.Add(new SqlParameter("@ColorIndex", colorIndex));
                    addSystemKpiCmd.Parameters.Add(new SqlParameter("@IsActiveInput", isActive));
                    addSystemKpiCmd.ExecuteNonQuery();

                    result = "Succes: KPI'en er nu tilføjet";
                }
                catch (SqlException e)
                {
                    return "Fejl: " + e.Message;
                }
            }

            return result;
        }

        public List<KPI> GetSystemKPI()
        {
            List<KPI> result = new List<KPI>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand getSystemKpiCmd = new SqlCommand("sp_GetSystemKPI", con);
                    getSystemKpiCmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = getSystemKpiCmd.ExecuteReader();

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

                            bool convertedIsActive;

                            if (isActive == "False")
                            {
                                convertedIsActive = false;
                            }
                            else
                            {
                                convertedIsActive = true;
                            }

                            result.Add(new KPI()
                            {
                                ID = convertedKpiID,
                                Title = kpiTitle,
                                Unit = kpiUnit,
                                Color = kpiColor,
                                ColorIndex = convertedColorIndex,
                                IsActive =  convertedIsActive
                            });
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message,"Fejl ved forbindelse til database", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return result;
        }
    }
}
