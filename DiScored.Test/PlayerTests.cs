using System;
using DiScored.Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiScored.Test
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void CreatePlayer_VerifyCorrectName_ScoreIsZero()
        {
            var player = new Player(Resources.HomerSimpson);
            Assert.AreEqual(Resources.HomerSimpson, player.Name);
            Assert.AreEqual(0, player.CurrentScore.Value);
        }

        [TestMethod]
        public void CreatePlayer_DontAllowNull_DontAllowEmpty()
        {
            try
            {
                new Player(null);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The name of a player is not allowed to be empty.", e.Message);
            }

            try
            {
                new Player(string.Empty);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The name of a player is not allowed to be empty.", e.Message);
            }
        }

        [TestMethod]
        public void AddScores_VerifyNewScore()
        {
            var player = new Player(Resources.HomerSimpson);

            player.AddScore(new Score(42));
            player.AddScore(new Score(-21));
            player.AddScore(new Score(0));
            player.AddScore(new Score(0.21));

            Assert.AreEqual(21.21, player.CurrentScore.Value);
        }

        [TestMethod]
        public void TwoPlayers_SameName_VerifyEqual()
        {
            var player1 = new Player(Resources.HomerSimpson);
            var player2 = new Player(Resources.HomerSimpson);

            Assert.AreEqual(player1, player2);
        }
    }
}