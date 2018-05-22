using System;
using System.Collections.Generic;
using System.Linq;

namespace RevisionFyn.BI_Pro.Model
{
    public class CustomStatistics
    {
        #region Variables / Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int ChoosenStatisticsTypeID { get; set; }
        public int ChoosenStatisticsCalculationID { get; set; }
        public List<Client> ChoosenClients { get; set; }
        #endregion

        #region Public methods
        public static double Average(List<double> inputData)
        {
            try
            {
                return Math.Round(inputData.Average(), 2);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        public static double Highest(List<double> inputData)
        {
            return inputData.Max();
        }

        public static double Lowest(List<double> inputData)
        {
            return inputData.Min();
        }

        public static double Count(List<double> inputData)
        {
            return inputData.Count();
        }

        public static double Sum(List<double> inputData)
        {
            return inputData.Sum();
        }
        #endregion
    }
}
