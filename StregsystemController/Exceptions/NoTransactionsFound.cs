using System;

namespace Stregsystem
{
    public class NoTransactionsFound : Exception
    {
        public NoTransactionsFound() : base("Could not find the transaction") { }
        public NoTransactionsFound(string message) : base(message) { }
    }
}
