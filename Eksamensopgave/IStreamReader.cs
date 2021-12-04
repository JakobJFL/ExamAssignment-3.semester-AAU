using System;

namespace Eksamensopgave
{
    public interface IStreamReader : IDisposable
    {
        public string ReadLine();
        public bool EndOfStream { get; }
    }
}
