using System;
using System.Collections.Generic;

namespace StringMetrics
{
    public abstract class StringMetricBase : IStringMetric
    {
        private readonly IEqualityComparer<char> equalityComparer;

        protected StringMetricBase()
            : this(EqualityComparer<char>.Default)
        {
        }

        protected StringMetricBase(IEqualityComparer<char> equalityComparer)
        {
            this.equalityComparer = equalityComparer ?? throw new ArgumentNullException(nameof(equalityComparer));
        }

        public int CalculateDistance(string one, string other)
        {
            if (one == null) throw new ArgumentNullException(nameof(one));
            if (other == null) throw new ArgumentNullException(nameof(other));

            return CalculateDistanceCore(one, other);
        }

        protected abstract int CalculateDistanceCore(string one, string other);

        protected bool AreEqual(char one, char other)
        {
            return equalityComparer.Equals(one, other);
        }
    }
}