using System;
using DiScored.Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiScored.Test
{
    [TestClass]
    public class ModeratorTests
    {
        [TestMethod]
        public void CreateModerator_VerifyCorrectName()
        {
            var moderator = new Moderator(Resources.HomerSimpson);
            Assert.AreEqual(Resources.HomerSimpson, moderator.Name);
        }

        [TestMethod]
        public void CreateModerator_DontAllowNull_DontAllowEmpty()
        {
            try
            {
                new Moderator(null);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The name of a moderator is not allowed to be empty.", e.Message);
            }

            try
            {
                new Moderator(string.Empty);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("The name of a moderator is not allowed to be empty.", e.Message);
            }
        }

        [TestMethod]
        public void TwoModerators_SameName_VerifyEqual()
        {
            var moderator1 = new Moderator(Resources.HomerSimpson);
            var moderator2 = new Moderator(Resources.HomerSimpson);

            Assert.AreEqual(moderator1, moderator2);
        }
    }
}