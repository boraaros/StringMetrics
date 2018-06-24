using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace StringMetrics.UnitTests
{
    [TestClass]
    public class LevenshteinTest
    {
        [TestMethod]
        public void Levenshtein_distance_ctor_throws_exception_if_equality_comparer_is_null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Levenshtein(null));
        }

        [TestMethod]
        public void Levenshtein_distance_throws_exception_if_input_string__is_null()
        {
            var levenshtein = new Levenshtein();
            Assert.ThrowsException<ArgumentNullException>(() => levenshtein.CalculateDistance("test", null));
            Assert.ThrowsException<ArgumentNullException>(() => levenshtein.CalculateDistance(null, "test"));
            Assert.ThrowsException<ArgumentNullException>(() => levenshtein.CalculateDistance(null, null));
        }

        [TestMethod]
        public void Levenshtein_distance_is_zero_if_the_two_string_are_equal()
        {
            var levenshtein = new Levenshtein();
            Assert.AreEqual(0, levenshtein.CalculateDistance("test", "test"));
        }

        [TestMethod]
        public void Levenshtein_distance_calculation_if_strings_are_different()
        {
            var levenshtein = new Levenshtein();
            Assert.AreEqual(1, levenshtein.CalculateDistance("test1", "test2"));
            Assert.AreEqual(1, levenshtein.CalculateDistance("test", "tst"));
            Assert.AreEqual(2, levenshtein.CalculateDistance("test", "tset"));
            Assert.AreEqual(2, levenshtein.CalculateDistance("test", "tste"));
            Assert.AreEqual(3, levenshtein.CalculateDistance("test", "etts"));
        }

        [TestMethod]
        public void Levenshtein_distance_calculation_with_specified_equality_comparer()
        {
            var equalityComparer = new Mock<IEqualityComparer<char>>(MockBehavior.Strict);
            equalityComparer
                .Setup(t => t.Equals(It.IsAny<char>(), It.IsAny<char>()))
                .Returns<char, char>((x, y) => x == 't' || y == 't');
            var levenshtein = new Levenshtein(equalityComparer.Object);
            Assert.AreEqual(2, levenshtein.CalculateDistance("test", "test"));
        }
    }
}