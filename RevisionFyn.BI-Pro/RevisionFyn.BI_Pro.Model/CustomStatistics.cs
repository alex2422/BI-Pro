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

        public double Average(List<double> listOfInputData)
        { 
            // if sql cannot extract value then send AccoundCard and
            // .Average(x => x.Balance)

            return listOfInputData.Average();
        }

        public double Highest(List<double> listOfInputData)
        {
            return listOfInputData.Max();
        }

        public double Lowest(List<double> listOfInputData)
        {
            return listOfInputData.Min();
        }

        public double Count(List<double> listOfInputData)
        {
            return listOfInputData.Count();
        }

        public double Sum(List<double> listOfInputData)
        {
            return listOfInputData.Sum();
        }
    }
}
