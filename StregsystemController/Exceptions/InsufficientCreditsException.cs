using System;

namespace Stregsystem
{
    public class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException() : base("User balance is not high enough") { }
        public InsufficientCreditsException(string message) : base(message) {}
    }
}
