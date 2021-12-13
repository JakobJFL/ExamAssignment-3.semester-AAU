using System;

namespace Stregsystem.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Could not find the product") { }
        public ProductNotFoundException(string message) : base(message) { }
    }
}
