using System;
using System.Collections.Generic;
using System.Linq;

namespace StringMetrics
{
    public class Hamming : StringMetricBase
    {
        public Hamming()
            : base()
        {
        }

        public Hamming(IEqualityComparer<char> equalityComparer)
            : base(equalityComparer)
        { 
        }

        protected override int CalculateDistanceCore(string one, string other)
        {
            ValidateLengthEquality(one, other);

            return one.Zip(other, (x, y) => AreEqual(x, y) ? 0 : 1).Sum();
        }

        private void ValidateLengthEquality(string one, string other)
        {
            if (one.Length != other.Length)
            {
                throw new ArgumentException();
            }
        }
    }
}