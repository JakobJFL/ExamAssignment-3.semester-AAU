using Stregsystem;

namespace Stregsystem
{
    public interface IStregsystemCommandParser
    {
        IStregsystem Stregsystem { get; }

        public void ParseCommand(string command);
    }
}