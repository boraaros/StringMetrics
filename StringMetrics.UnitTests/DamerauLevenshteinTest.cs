using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace StringMetrics.UnitTests
{
    [TestClass]
    public class DamerauLevenshteinTest
    {
        [TestMethod]
        public void DamerauLevenshtein_distance_ctor_throws_exception_if_equality_comparer_is_null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DamerauLevenshtein(null));
        }

        [TestMethod]
        public void DamerauLevenshtein_distance_throws_exception_if_input_string__is_null()
        {
            var damerauLevenshtein = new DamerauLevenshtein();
            Assert.ThrowsException<ArgumentNullException>(() => damerauLevenshtein.CalculateDistance("test", null));
            Assert.ThrowsException<ArgumentNullException>(() => damerauLevenshtein.CalculateDistance(null, "test"));
            Assert.ThrowsException<ArgumentNullException>(() => damerauLevenshtein.CalculateDistance(null, null));
        }

        [TestMethod]
        public void DamerauLevenshtein_distance_is_zero_if_the_two_string_are_equal()
        {
            var damerauLevenshtein = new DamerauLevenshtein();
            Assert.AreEqual(0, damerauLevenshtein.CalculateDistance("test", "test"));
        }

        [TestMethod]
        public void DamerauLevenshtein_distance_calculation_if_strings_are_different()
        {
            var damerauLevenshtein = new DamerauLevenshtein();
            Assert.AreEqual(1, damerauLevenshtein.CalculateDistance("test1", "test2"));
            Assert.AreEqual(1, damerauLevenshtein.CalculateDistance("test", "tst"));
            Assert.AreEqual(1, damerauLevenshtein.CalculateDistance("test", "tset"));
            Assert.AreEqual(2, damerauLevenshtein.CalculateDistance("test", "tste"));
            Assert.AreEqual(2, damerauLevenshtein.CalculateDistance("test", "etts"));
        }

        [TestMethod]
        public void DamerauLevenshtein_distance_calculation_with_specified_equality_comparer()
        {
            var equalityComparer = new Mock<IEqualityComparer<char>>(MockBehavior.Strict);
            equalityComparer
                .Setup(t => t.Equals(It.IsAny<char>(), It.IsAny<char>()))
                .Returns<char, char>((x, y) => x == 't' || y == 't');
            var damerauLevenshtein = new DamerauLevenshtein(equalityComparer.Object);
            Assert.AreEqual(2, damerauLevenshtein.CalculateDistance("test", "test"));
        }
    }
}