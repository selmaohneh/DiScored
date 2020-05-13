using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiScored.Test
{
    [TestClass]
    public class ScoreTests
    {
        [TestMethod]
        public void CreateScoreWithValue_VerifyCorrectValue()
        {
            const double testValue = 42.42;
            var score = new Score(testValue);

            Assert.AreEqual(testValue, score.Value);
        }

        [TestMethod]
        public void DontAllowScoreNaN()
        {
            const double testValue = double.NaN;

            try
            {
                new Score(testValue);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The value of score is not allowed to be double.NaN.", e.Message);
            }
        }

        [TestMethod]
        public void CreateScoreFromMultipleScores_VerifyNewScoreIsSum()
        {
            var scores = new List<Score>
            {
                new Score(21.21),
                new Score(42.42),
                new Score(99.99)
            };

            var score = new Score(scores);

            Assert.AreEqual(21.21 + 42.42 + 99.99, score.Value);
        }

        [TestMethod]
        public void SameValue_ScoresEqual()
        {
            var score1 = new Score(42.42);
            var score2 = new Score(42.42);

            Assert.AreEqual(score1, score2);
        }
    }
}