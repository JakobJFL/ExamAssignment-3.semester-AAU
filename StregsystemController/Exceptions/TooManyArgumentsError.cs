using System;

namespace Stregsystem.Exceptions
{
    public class TooManyArgumentsError : Exception
    {
        public TooManyArgumentsError() : base("Too many arguments was for the command was given") { }
        public TooManyArgumentsError(string message) : base(message) { }
    }

    

}
