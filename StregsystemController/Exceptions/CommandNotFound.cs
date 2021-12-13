using System;

namespace Stregsystem.Exceptions
{
    public class CommandNotFound : Exception
    {
        public CommandNotFound() : base("Could not find the admin command") { }
        public CommandNotFound(string message) : base(message) { }
    }
}
