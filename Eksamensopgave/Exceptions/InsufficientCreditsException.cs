using System;

namespace Eksamensopgave
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(string message) : base(message) {}
    }
}
