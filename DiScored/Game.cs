using System;
using System.Collections.Generic;
using System.Linq;

namespace DiScored
{
    public class Game
    {
        public Moderator Moderator { get; }
        public IEnumerable<Player> Players => _players.AsEnumerable();
        private readonly List<Player> _players;

        public Game(Moderator moderator, IReadOnlyList<Player> players)
        {
            Moderator = moderator;

            var hasDuplicates = players.GroupBy(p => p).Any(g => g.Count() > 1);
            if (hasDuplicates)
            {
                throw new ArgumentException(
                    "Can't create a game with duplicated players. Make sure your players have unique names.");
            }

            if (players.Count < 1)
            {
                throw new ArgumentException("You need at least one player to create a new game.");
            }

            _players = players.ToList();
        }
    }
}