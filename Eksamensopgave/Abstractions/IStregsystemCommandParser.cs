using Stregsystem;
using Stregsystem.Abstractions;

namespace StregsystemController.Abstractions
{
    public interface IStregsystemCommandParser
    {
        IStregsystemHandler Stregsystem { get; }

        public void ParseCommand(string command);
    }
}