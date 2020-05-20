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
        public async Task ScoreAsync(string playerName, double points = 1)
        {
            var moderator = new Moderator(Context.User.Username);
            var player = new Player(playerName);
            var score = new Score(points);
            await _gameCommander.Score(moderator, player, score);
        }

        [Command("standings")]
        public async Task StandingsAsync()
        {
            var moderator = new Moderator(Context.User.Username);
            await _gameCommander.Standings(moderator);
        }
    }
}