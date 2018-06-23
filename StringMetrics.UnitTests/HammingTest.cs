using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace StringMetrics.UnitTests
{
    [TestClass]
    public class HammingTest
    {
        [TestMethod]
        public void Hamming_distance_ctor_throws_exception_if_equality_comparer_is_null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Hamming(null));
        }

        [TestMethod]
        public void Hamming_distance_throws_exception_if_input_string__is_null()
        {
            var hamming = new Hamming();
            Assert.ThrowsException<ArgumentNullException>(() => hamming.CalculateDistance("test", null));
            Assert.ThrowsException<ArgumentNullException>(() => hamming.CalculateDistance(null, "test"));
            Assert.ThrowsException<ArgumentNullException>(() => hamming.CalculateDistance(null, null));
        }

        [TestMethod]
        public void Hamming_distance_throws_exception_if_length_of_strings_are_different()
        {
            var hamming = new Hamming();
            Assert.ThrowsException<ArgumentException>(() => hamming.CalculateDistance("test", ""));
            Assert.ThrowsException<ArgumentException>(() => hamming.CalculateDistance("", "test"));
            Assert.ThrowsException<ArgumentException>(() => hamming.CalculateDistance("test", "tes"));
            Assert.ThrowsException<ArgumentException>(() => hamming.CalculateDistance("tes", "test"));
        }

        [TestMethod]
        public void Hamming_distance_is_zero_if_the_two_string_are_equal()
        {
            var hamming = new Hamming();
            Assert.AreEqual(0, hamming.CalculateDistance("test", "test"));
        }

        [TestMethod]
        public void Hamming_distance_calculation_if_strings_are_different()
        {
            var hamming = new Hamming();
            Assert.AreEqual(1, hamming.CalculateDistance("test1", "test2"));
            Assert.AreEqual(2, hamming.CalculateDistance("test", "tset"));
            Assert.AreEqual(3, hamming.CalculateDistance("test", "tste"));
            Assert.AreEqual(4, hamming.CalculateDistance("test", "etts"));
        }

        [TestMethod]
        public void Hamming_distance_calculation_with_specified_equality_comparer()
        {
            var equalityComparer = new Mock<IEqualityComparer<char>>(MockBehavior.Strict);
            equalityComparer.Setup(t => t.Equals('t', 't')).Returns(true);
            equalityComparer.Setup(t => t.Equals('e', 'e')).Returns(false);
            equalityComparer.Setup(t => t.Equals('s', 's')).Returns(false);
            var hamming = new Hamming(equalityComparer.Object);
            Assert.AreEqual(2, hamming.CalculateDistance("test", "test"));
        }
    }
}