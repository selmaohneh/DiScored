using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiScored
{
    public class GameCommander
    {
        private readonly IMarkdownOutput _output;
        private Game _game;

        public GameCommander(IMarkdownOutput output)
        {
            _output = output;
        }

        public async Task New(Moderator moderator, params Player[] players)
        {
            _game = new Game(moderator, players);

            await _output.Write(
                $"**{moderator.Name}** created a game with the player(s) {BuildPlayerString(players)}.");
        }

        public async Task Score(Moderator moderator, Player player, Score score)
        {
            EnsureAGameWasCreated();
            EnsureAllowedToAddScore(moderator);


            var foundPlayer = _game.Players.SingleOrDefault(p => p.Equals(player));
            if (foundPlayer == null)
            {
                throw new ArgumentException($"There is no player with the name **{player.Name}** in the current game.");
            }

            foundPlayer.AddScore(score);

            await _output.Write($"**{moderator.Name}** added **{score.Value}** point(s) to **{player.Name}**.");
        }

        private void EnsureAllowedToAddScore(Moderator moderator)
        {
            if (!moderator.Equals(_game.Moderator))
            {
                throw new ArgumentException(
                    $"Only **{_game.Moderator.Name}** is allowed to add points in the current game.");
            }
        }

        public async Task Standings(Moderator moderator)
        {
            EnsureAGameWasCreated();
            EnsureAllowedToShowStandings(moderator);
            var sortedPlayers = _game.Players.OrderByDescending(x => x.CurrentScore.Value).ToList();

            await _output.Write($"**{moderator.Name}** asked for the current standings:");
            for (var i = 0; i < sortedPlayers.Count; i++)
            {
                await _output.Write(
                    $"{i + 1}. is **{sortedPlayers[i].Name}** with **{sortedPlayers[i].CurrentScore.Value}** point(s).");
            }
        }


        private void EnsureAllowedToShowStandings(Moderator moderator)
        {
            if (!moderator.Equals(_game.Moderator))
            {
                throw new ArgumentException(
                    $"Only **{_game.Moderator.Name}** is allowed to show the current standings.");
            }
        }


        private string BuildPlayerString(params Player[] players)
        {
            var playerString = string.Empty;
            foreach (var player in players)
            {
                playerString += $"**{player.Name}**";
                if (!player.Equals(players.Last()))
                {
                    playerString += ", ";
                }
            }

            return playerString;
        }

        private void EnsureAGameWasCreated()
        {
            if (_game == null)
            {
                throw new NullReferenceException("There was no game created yet.");
            }
        }
    }
}