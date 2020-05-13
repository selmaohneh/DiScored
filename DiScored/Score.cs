using System;
using System.Collections.Generic;
using System.Linq;

namespace DiScored
{
    public class Score
    {
        public double Value { get; }

        public Score(double value)
        {
            if (double.IsNaN(value))
            {
                throw new ArgumentException("The value of score is not allowed to be double.NaN.");
            }

            Value = value;
        }

        public Score(IEnumerable<Score> scores)
        {
            Value = scores.Sum(s => s.Value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Score score))
            {
                return false;
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return score.Value == Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
