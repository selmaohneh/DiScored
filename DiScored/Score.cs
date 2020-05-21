using System;
using System.Collections.Generic;
using System.Linq;

namespace DiScored
{
    public class Score
    {
        public double Value { get; }
        public string Comment { get; }

        public Score(double value, string comment = "")
        {
            if (double.IsNaN(value))
            {
                throw new ArgumentException("The value of score is not allowed to be double.NaN.");
            }

            Value = value;
            Comment = comment ?? throw new ArgumentException("The comment of score is not allowed to be null.");
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
