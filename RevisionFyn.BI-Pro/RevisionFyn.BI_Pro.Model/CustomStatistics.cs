using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class CustomStatistics
    {
        public string Name { get; set; }
        public int ChoosenStatisticsTypeID { get; set; }
        public int ChoosenStatisticsCalculationID { get; set; }
        public List<Company> ChoosenCompanies { get; set; }

        public double Average(List<double> listOfInputData)
        {
            return listOfInputData.Average();
        }

        public double Highest()
        {
            throw new NotImplementedException();
        }

        public double Lowest()
        {
            throw new NotImplementedException();
        }

        public double Count()
        {
            throw new NotImplementedException();
        }
    }
}
