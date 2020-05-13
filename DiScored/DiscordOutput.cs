using System;
using System.Threading.Tasks;
using Discord;

namespace DiScored
{
    public class DiscordOutput : IMarkdownOutput
    {
        private IMessageChannel _messageChannel;

        public void SetChannel(IMessageChannel channel)
        {
            _messageChannel = channel;
        }

        public async Task Write(string text)
        {
            if (_messageChannel == null)
            {
                throw new Exception("No target channel was set for the output.");
            }

            await _messageChannel.SendMessageAsync(text);
        }
    }
}