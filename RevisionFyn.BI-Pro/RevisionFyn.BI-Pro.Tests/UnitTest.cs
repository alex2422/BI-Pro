using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Tests
{
    [TestClass]
    public class UnitTest
    {
        #region CustomStatistics - Average
        [TestMethod]
        public void Should_ComputeAverage_When_PositiveListProvided()
        {
            List<double> listOfData = new List<double>() { 10, 5, 3, 6 };

            Assert.AreEqual(6, CustomStatistics.Average(listOfData));
        }

        [TestMethod]
        public void Should_ComputeeAverage_When_NegativeListProvided()
        {
            List<double> listOfData = new List<double>() { -10, -5, -3, -6 };

            Assert.AreEqual(-6, CustomStatistics.Average(listOfData));
        }

        [TestMethod]
        public void Should_ComputeAverage_When_DecimalListProvided()
        {
            List<double> listOfData = new List<double>() { 10.0, 5, 3, 6.0, 4.5 };

            Assert.AreEqual(5.7, CustomStatistics.Average(listOfData));
        }

        [TestMethod]
        public void Should_ComputeAverage_When_OverTwoDecimalListProvided()
        {
            List<double> listOfData = new List<double>() { 10.0, 5, 3, 6.78, 4.5 };

            Assert.AreEqual(5.86, CustomStatistics.Average(listOfData));
        }

        [TestMethod]
        public void Should_ReturnZeroAverage_When_EmptyListProvided()
        {
            List<double> listOfData = new List<double>() { };

            Assert.AreEqual(0, CustomStatistics.Average(listOfData));
        }
        #endregion
    }
}
