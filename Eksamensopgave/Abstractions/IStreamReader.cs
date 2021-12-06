using System;

namespace Stregsystem
{
    public interface IStreamReader : IDisposable
    {
        public string ReadLine();
        public bool EndOfStream { get; }
    }
}
