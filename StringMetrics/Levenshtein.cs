using System;
using System.Collections.Generic;
using System.Linq;

namespace StringMetrics
{
    public class Levenshtein : StringMetricBase
    {
        public Levenshtein()
            : base()
        {
        }

        public Levenshtein(IEqualityComparer<char> equalityComparer)
            : base(equalityComparer)
        {
        }

        protected override int CalculateDistanceCore(string one, string other)
        {
            return RecursiveCalculation(one, other, one.Length, other.Length);
        }

        private int RecursiveCalculation(string one, string other, int oneSubLength, int otherSubLength)
        {
            if (oneSubLength == 0 || otherSubLength == 0)
            {
                return Math.Max(oneSubLength, otherSubLength);
            }

            return new List<int>
            {
                Insert(one, other, oneSubLength, otherSubLength),
                Delete(one, other, oneSubLength, otherSubLength),
                Substitute(one, other, oneSubLength, otherSubLength)
            }.Min();
        }

        private int Insert(string one, string other, int oneSubLength, int otherSubLength)
        {
            return RecursiveCalculation(one, other, oneSubLength - 1, otherSubLength) + 1;
        }

        private int Delete(string one, string other, int oneSubLength, int otherSubLength)
        {
            return RecursiveCalculation(one, other, oneSubLength, otherSubLength - 1) + 1;
        }

        private int Substitute(string one, string other, int oneSubLength, int otherSubLength)
        {
            return RecursiveCalculation(one, other, oneSubLength - 1, otherSubLength - 1)
                 + (AreEqual(one[oneSubLength - 1], other[otherSubLength - 1]) ? 0 : 1);
        }
    }
}