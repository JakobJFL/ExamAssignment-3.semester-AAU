using System;

namespace Stregsystem.Exceptions
{
    public class AdminCommandNotFound : Exception
    {
        public AdminCommandNotFound() : base("Could not find the admin command") { }
        public AdminCommandNotFound(string message) : base(message) { }
    }
}
