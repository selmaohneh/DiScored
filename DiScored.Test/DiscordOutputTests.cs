using System.Threading.Tasks;
using Discord;
using DiScored.Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DiScored.Test
{
    [TestClass]
    public class DiscordOutputTests
    {
        [TestMethod]
        public async Task WriteToOutput_SendMessageAsync()
        {
            var messageChannel = new Mock<IMessageChannel>();
            var discordOutput = new DiscordOutput();

            discordOutput.SetChannel(messageChannel.Object);

            await discordOutput.Write(Resources.HomerSimpson);

            messageChannel.Verify(mc => mc.SendMessageAsync(Resources.HomerSimpson, false, null, null));
        }
    }
}