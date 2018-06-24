using System;
using System.Collections.Generic;
using System.Linq;

namespace StringMetrics
{
    public class DamerauLevenshtein : Levenshtein
    {
        public DamerauLevenshtein()
            : base()
        {
        }

        public DamerauLevenshtein(IEqualityComparer<char> equalityComparer)
            : base(equalityComparer)
        {
        }

        protected override int RecursiveCalculation(string one, string other, int onePrefixLength, int otherPrefixLength)
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

            if (IsTransposable(one, other, onePrefixLength, otherPrefixLength))
            {
                cases.Add(Transposition(one, other, onePrefixLength, otherPrefixLength));
            }

            return cases.Min();
        }

        private bool IsTransposable(string one, string other, int onePrefixLength, int otherPrefixLength)
        {
            return onePrefixLength > 1 
                && otherPrefixLength > 1 
                && AreEqual(one[onePrefixLength - 1], other[otherPrefixLength - 2]) 
                && AreEqual(one[onePrefixLength - 2], other[otherPrefixLength - 1]);
        }

        private int Transposition(string one, string other, int onePrefixLength, int otherPrefixLength)
        {
            return RecursiveCalculation(one, other, onePrefixLength - 2, otherPrefixLength - 2) + 1;
        }
    }
}