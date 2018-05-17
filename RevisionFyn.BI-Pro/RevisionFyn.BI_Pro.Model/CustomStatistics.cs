using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class CustomStatistics
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ChoosenStatisticsTypeID { get; set; }
        public int ChoosenStatisticsCalculationID { get; set; }
        public List<Company> ChoosenCompanies { get; set; }

        public static double Average(List<double> listOfInputData)
        {
            return listOfInputData.Average();
        }

        public static double Highest(List<double> listOfInputData)
        {
            return listOfInputData.Max();
        }

        public static double Lowest(List<double> listOfInputData)
        {
            return listOfInputData.Min();
        }

        public static double Count(List<double> listOfInputData)
        {
            return listOfInputData.Count();
        }

        public static double Sum(List<double> listOfInputData)
        {
            return listOfInputData.Sum();
        }
    }
}
