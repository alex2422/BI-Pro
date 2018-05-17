using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Tests
{
    [TestClass]
    public class UnitTest
    {
        // TO DO: Test negative listOfData
        // TO DO: Test long deciamal listOfData
        // TO DO: Test empty listOfData

        [TestMethod]
        public void Should_CalculateAverage_When_StraightListProvided()
        {
            //CustomStatistics customStatistics = new CustomStatistics();

            List<double> listOfData = new List<double>() { 10, 5, 3, 6};

            Assert.AreEqual(6, CustomStatistics.Average(listOfData));
        }

        [TestMethod]
        public void Should_CalculateAverage_When_DecimalListProvided()
        {
            //CustomStatistics customStatistics = new CustomStatistics();

            List<double> listOfData = new List<double>() { 10.0, 5, 3, 6.0, 4.5 };

            Assert.AreEqual(5.7, CustomStatistics.Average(listOfData));
        }
    }
}
