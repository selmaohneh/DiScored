using System;
using System.Collections.Generic;
using System.Linq;

namespace DiScored
{
    public class Player : IEquatable<Player>
    {
        public string Name { get; }
        public Score CurrentScore => new Score(_scores.Select(s=>s.Value).Sum());

        private readonly List<Score> _scores = new List<Score>();

        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name of a player is not allowed to be empty.");
            }

            Name = name;
        }

        public void AddScore(Score score)
        {
            _scores.Add(score);
        }

        public bool Equals(Player other)
        {
            return other?.Name == Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Player player))
            {
                return false;
            }

            return Equals(player);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}