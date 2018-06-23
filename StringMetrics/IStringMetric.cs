using System;

namespace StringMetrics
{
    public interface IStringMetric
    {
        int CalculateDistance(string one, string other);
    }
}