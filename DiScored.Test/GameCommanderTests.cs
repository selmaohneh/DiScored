using System;
using System.Threading.Tasks;
using DiScored.Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DiScored.Test
{
    [TestClass]
    public class GameCommanderTests
    {
        [TestMethod]
        public async Task NewGame_ThreePlayers_CorrectOutput()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator = new Moderator(Resources.HomerSimpson);
            var player1 = new Player(Resources.HomerSimpson);
            var player2 = new Player(Resources.MargeSimpson);
            var player3 = new Player(Resources.BartSimpson);

            await sut.New(moderator, player1, player2, player3);

            var expectedOutput =
                "**Homer Simpson** created a game with the player(s) **Homer Simpson**, **Marge Simpson**, **Bart Simpson**.";

            output.Verify(x => x.Write(expectedOutput));
        }


        [TestMethod]
        public async Task NewGame_SinglePlayer_CorrectOutput()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator = new Moderator(Resources.HomerSimpson);
            var player = new Player(Resources.HomerSimpson);

            await sut.New(moderator, player);

            var expectedOutput =
                "**Homer Simpson** created a game with the player(s) **Homer Simpson**.";

            output.Verify(x => x.Write(expectedOutput));
        }

        [TestMethod]
        public async Task AddScore_PlayerDoesntExist_ThrowException()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator = new Moderator(Resources.HomerSimpson);
            var player1 = new Player(Resources.HomerSimpson);
            var player2 = new Player(Resources.MargeSimpson);
            var player3 = new Player(Resources.BartSimpson);
            await sut.New(moderator, player1, player2);

            try
            {
                await sut.Score(moderator, player3, new Score(42));
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("There is no player with the name **Bart Simpson** in the current game.", e.Message);
            }
        }


        [TestMethod]
        public async Task AddScore_CorrectOutput()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator = new Moderator(Resources.HomerSimpson);
            var player1 = new Player(Resources.HomerSimpson);
            var player2 = new Player(Resources.MargeSimpson);
            var player3 = new Player(Resources.BartSimpson);

            await sut.New(moderator, player1, player2, player3);
            await sut.Score(moderator, player2, new Score(42));

            var expectedOutput =
                "**Homer Simpson** added **42** point(s) to **Marge Simpson**.";

            output.Verify(x => x.Write(expectedOutput));
        }

        [TestMethod]
        public async Task AddScore_WithComment_CorrectOutput()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator = new Moderator(Resources.HomerSimpson);
            var player1 = new Player(Resources.HomerSimpson);
            var player2 = new Player(Resources.MargeSimpson);
            var player3 = new Player(Resources.BartSimpson);

            await sut.New(moderator, player1, player2, player3);
            await sut.Score(moderator, player2, new Score(42, "Question 3"));

            var expectedOutput =
                "**Homer Simpson** added **42** point(s) to **Marge Simpson**. Comment: **Question 3**.";

            output.Verify(x => x.Write(expectedOutput));
        }

        [TestMethod]
        public async Task Standings_CorrectOutput()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator = new Moderator(Resources.HomerSimpson);
            var player1 = new Player(Resources.HomerSimpson);
            var player2 = new Player(Resources.MargeSimpson);
            var player3 = new Player(Resources.BartSimpson);

            await sut.New(moderator, player1, player2, player3);
            await sut.Score(moderator, player1, new Score(21));
            await sut.Score(moderator, player2, new Score(99));
            await sut.Score(moderator, player3, new Score(42));
            await sut.Standings(moderator);

            output.Verify(x => x.Write("**Homer Simpson** asked for the current standings:"));
            output.Verify(x => x.Write("1. is **Marge Simpson** with **99** point(s)."));
            output.Verify(x => x.Write("2. is **Bart Simpson** with **42** point(s)."));
            output.Verify(x => x.Write("3. is **Homer Simpson** with **21** point(s)."));
        }

        [TestMethod]
        public async Task NoGameYet_ThrowException()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator = new Moderator(Resources.HomerSimpson);
            var player = new Player(Resources.HomerSimpson);
            var score = new Score(42);

            try
            {
                await sut.Score(moderator, player, score);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("There was no game created yet.", e.Message);
            }

            try
            {
                await sut.Standings(moderator);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("There was no game created yet.", e.Message);
            }
        }

        [TestMethod]
        public async Task Score_NotModerator_ThrowException()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator1 = new Moderator(Resources.HomerSimpson);
            var moderator2 = new Moderator(Resources.MargeSimpson);
            var player = new Player(Resources.HomerSimpson);
            var score = new Score(42);

            await sut.New(moderator1, player);
            try
            {
                await sut.Score(moderator2, player, score);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Only **Homer Simpson** is allowed to add points in the current game.", e.Message);
            }
        }

        [TestMethod]
        public async Task Standings_NotModerator_ThrowException()
        {
            var output = new Mock<IMarkdownOutput>();
            var sut = new GameCommander(output.Object);

            var moderator1 = new Moderator(Resources.HomerSimpson);
            var moderator2 = new Moderator(Resources.MargeSimpson);
            var player = new Player(Resources.HomerSimpson);

            await sut.New(moderator1, player);
            try
            {
                await sut.Standings(moderator2);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Only **Homer Simpson** is allowed to show the current standings.", e.Message);
            }
        }
    }
}