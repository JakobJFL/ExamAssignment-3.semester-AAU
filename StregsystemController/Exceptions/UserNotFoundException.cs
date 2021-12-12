using System;

namespace Stregsystem
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Could not find the user") {}
        public UserNotFoundException(string message) : base(message) { }
    }
}
