using System;

namespace Stregsystem.Abstractions
{
    public interface IStreamReader : IDisposable
    {
        public string ReadLine();
        public bool EndOfStream { get; }
    }
}
