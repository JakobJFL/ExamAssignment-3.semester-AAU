using Stregsystem;
using Stregsystem.Abstractions;

namespace Stregsystem
{
    public interface IStregsystemCommandParser
    {
        IStregsystemHandler Stregsystem { get; }

        public void ParseCommand(string command);
    }
}