using System.Threading.Tasks;

namespace DiScored
{
    public interface IMarkdownOutput
    {
        Task Write(string text);
    }
}