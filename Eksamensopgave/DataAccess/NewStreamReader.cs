using System.IO;

namespace Eksamensopgave
{
    public class NewStreamReader : StreamReader, IStreamReader
    {
        public NewStreamReader(string filePath) : base (filePath) { }
    }
}
