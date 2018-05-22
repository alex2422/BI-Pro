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
        public void Should_ComputeAverage_When_NegativeListProvided()
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

        #region CustomStatistics - Highest
        [TestMethod]
        public void Should_ComputeHighest_When_PositiveListProvided()
        {
            List<double> listOfData = new List<double>() { 10, 5, 3, 6 };

            Assert.AreEqual(10, CustomStatistics.Highest(listOfData));
        }

        [TestMethod]
        public void Should_ComputeHighest_When_NegativeListProvided()
        {
            List<double> listOfData = new List<double>() { -10, -5, -3, -6 };

            Assert.AreEqual(-3, CustomStatistics.Highest(listOfData));
        }

        [TestMethod]
        public void Should_ComputeHighest_When_DecimalListProvided()
        {
            List<double> listOfData = new List<double>() { 10.2, 5, 3, 6.0, 4.5 };

            Assert.AreEqual(10.2, CustomStatistics.Highest(listOfData));
        }

        [TestMethod]
        public void Should_ReturnZeroHighest_When_EmptyListProvided()
        {
            List<double> listOfData = new List<double>() { };

            Assert.AreEqual(0, CustomStatistics.Highest(listOfData));
        }
        #endregion

        #region CustomStatistics - Lowest
        [TestMethod]
        public void Should_ComputeLowest_When_PositiveListProvided()
        {
            List<double> listOfData = new List<double>() { 10, 5, 3, 6 };

            Assert.AreEqual(3, CustomStatistics.Lowest(listOfData));
        }

        [TestMethod]
        public void Should_ComputeLowest_When_NegativeListProvided()
        {
            List<double> listOfData = new List<double>() { -10, -5, -3, -6 };

            Assert.AreEqual(-10, CustomStatistics.Lowest(listOfData));
        }

        [TestMethod]
        public void Should_ComputeLowest_When_DecimalListProvided()
        {
            List<double> listOfData = new List<double>() { 10.2, 5, 3.6, 6.0, 4.5 };

            Assert.AreEqual(3.6, CustomStatistics.Lowest(listOfData));
        }

        [TestMethod]
        public void Should_ReturnZeroLowest_When_EmptyListProvided()
        {
            List<double> listOfData = new List<double>() { };

            Assert.AreEqual(0, CustomStatistics.Lowest(listOfData));
        }
        #endregion

        #region CustomStatistics - Count
        [TestMethod]
        public void Should_Count_When_ListProvided()
        {
            List<double> listOfData = new List<double>() { 10, 5, 3, 6 };

            Assert.AreEqual(4, CustomStatistics.Count(listOfData));
        }

        [TestMethod]
        public void Should_ReturnZeroCount_When_EmptyListProvided()
        {
            List<double> listOfData = new List<double>() { };

            Assert.AreEqual(0, CustomStatistics.Count(listOfData));
        }
        #endregion

        #region CustomStatistics - Sum
        [TestMethod]
        public void Should_ComputeSum_When_PositiveListProvided()
        {
            List<double> listOfData = new List<double>() { 10, 5, 3, 6 };

            Assert.AreEqual(24, CustomStatistics.Sum(listOfData));
        }

        [TestMethod]
        public void Should_ComputeSum_When_NegativeListProvided()
        {
            List<double> listOfData = new List<double>() { -10, -5, -3, -6 };

            Assert.AreEqual(-24, CustomStatistics.Sum(listOfData));
        }

        [TestMethod]
        public void Should_ComputeSum_When_DecimalListProvided()
        {
            List<double> listOfData = new List<double>() { 10.2, 5, 3.6, 6.0, 4.5 };

            Assert.AreEqual(29.3, CustomStatistics.Sum(listOfData));
        }

        [TestMethod]
        public void Should_ReturnZeroSum_When_EmptyListProvided()
        {
            List<double> listOfData = new List<double>() { };

            Assert.AreEqual(0, CustomStatistics.Sum(listOfData));
        }
        #endregion
    }
}
