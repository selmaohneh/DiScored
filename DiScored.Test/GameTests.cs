using System;
using System.Collections.Generic;
using System.Linq;
using DiScored.Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiScored.Test
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void CreateGameWithThreePlayers_CorrectPlayers_CorrectModerator()
        {
            var player1 = new Player(Resources.HomerSimpson);
            var player2 = new Player(Resources.MargeSimpson);
            var player3 = new Player(Resources.BartSimpson);

            var moderator = new Moderator(Resources.HomerSimpson);

            var players = new List<Player>
            {
                player1, player2, player3
            };

            var game = new Game(moderator, players);

            CollectionAssert.AreEqual(players, game.Players.ToList());
            Assert.AreEqual(moderator, game.Moderator);
        }

        [TestMethod]
        public void DontAllowDuplicatePlayers()
        {
            var player1 = new Player(Resources.HomerSimpson);
            var player2 = new Player(Resources.HomerSimpson);
            var moderator = new Moderator(Resources.HomerSimpson);

            var players = new List<Player>
            {
                player1, player2
            };
            try
            {
                new Game(moderator, players);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(
                    "Can't create a game with duplicated players. Make sure your players have unique names.",
                    e.Message);
            }
        }

        [TestMethod]
        public void DontAllowZeroPlayers()
        {
            var moderator = new Moderator(Resources.HomerSimpson);
            try
            {
                new Game(moderator, new List<Player>());
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(
                    "You need at least one player to create a new game.",
                    e.Message);
            }
        }
    }
}