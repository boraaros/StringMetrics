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

        protected virtual int RecursiveCalculation(string one, string other, int onePrefixLength, int otherPrefixLength)
        {
            if (onePrefixLength == 0 || otherPrefixLength == 0)
            {
                return Math.Max(onePrefixLength, otherPrefixLength);
            }

            IList<int> cases = new List<int>
            {
                Insertion(one, other, onePrefixLength, otherPrefixLength),
                Deletion(one, other, onePrefixLength, otherPrefixLength),
                Substitution(one, other, onePrefixLength, otherPrefixLength)
            };

            return cases.Min();
        }

        protected int Insertion(string one, string other, int onePrefixLength, int otherPrefixLength)
        {
            return RecursiveCalculation(one, other, onePrefixLength - 1, otherPrefixLength) + 1;
        }

        protected int Deletion(string one, string other, int onePrefixLength, int otherPrefixLength)
        {
            return RecursiveCalculation(one, other, onePrefixLength, otherPrefixLength - 1) + 1;
        }

        protected int Substitution(string one, string other, int onePrefixLength, int otherPrefixLength)
        {
            return RecursiveCalculation(one, other, onePrefixLength - 1, otherPrefixLength - 1)
                 + (AreEqual(one[onePrefixLength - 1], other[otherPrefixLength - 1]) ? 0 : 1);
        }
    }
}