using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevisionFyn.BI_Pro.Database;

namespace RevisionFyn.BI_Pro.Controller
{
    public class StoredProcedureController
    {
        public string C_AddSystemKPI(string kpiTitle, string kpiUnit, string kpiColor, int colorIndex, string isActive)
        {
            StoredProcedure sp = new StoredProcedure();
            return sp.AddSystemKPI(kpiTitle, kpiUnit, kpiColor, colorIndex, isActive);
        }
    }
}
