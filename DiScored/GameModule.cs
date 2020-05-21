using System.Linq;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiScored
{
    public class GameModule : ModuleBase<SocketCommandContext>
    {
        private readonly GameCommander _gameCommander;

        public GameModule(GameCommander gameCommander)
        {
            _gameCommander = gameCommander;
        }

        [Command("new")]
        public async Task New(params string[] playerNames)
        {
            var moderator = new Moderator(Context.User.Username);
            var players = playerNames.Select(p => new Player(p)).ToArray();
            await _gameCommander.New(moderator, players);
        }

        [Command("score")]
        public async Task ScoreAsync(string playerName, double points, [Remainder] string comment)
        {
            await Score(playerName, points, comment);
        }

        [Command("score")]
        public async Task ScoreAsync(string playerName, double points)
        {
            await Score(playerName, points, "");
        }

        [Command("score")]
        public async Task ScoreAsync(string playerName)
        {
            await Score(playerName, 1, "");
        }

        [Command("score")]
        public async Task ScoreAsync(string playerName, [Remainder] string comment)
        {
            await Score(playerName, 1, comment);
        }

        [Command("standings")]
        public async Task StandingsAsync()
        {
            var moderator = new Moderator(Context.User.Username);
            await _gameCommander.Standings(moderator);
        }

        private async Task Score(string playerName, double points, string comment)
        {
            var moderator = new Moderator(Context.User.Username);
            var player = new Player(playerName);
            var score = new Score(points, comment);
            await _gameCommander.Score(moderator, player, score);
        }
    }
}