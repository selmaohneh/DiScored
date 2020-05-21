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
        public void DontAllowCommentNull()
        {
            const string testValue = null;

            try
            {
                new Score(42, testValue);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The comment of score is not allowed to be null.", e.Message);
            }
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