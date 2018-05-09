using System;
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

                    SqlCommand AddOffSupCatCmd = new SqlCommand("sp_AddSystemKPI", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    AddOffSupCatCmd.Parameters.Add(new SqlParameter("@Title", kpiTitle));
                    AddOffSupCatCmd.Parameters.Add(new SqlParameter("@Unit", kpiUnit));
                    AddOffSupCatCmd.Parameters.Add(new SqlParameter("@Color", kpiColor));
                    AddOffSupCatCmd.Parameters.Add(new SqlParameter("@ColorIndex", colorIndex));
                    AddOffSupCatCmd.Parameters.Add(new SqlParameter("@IsActiveInput", isActive));
                    AddOffSupCatCmd.ExecuteNonQuery();

                    result = "Succes: KPI'en er nu tilføjet";
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
