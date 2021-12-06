using System;

namespace Stregsystem
{
    public class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(string message) : base(message) {}
    }
}
