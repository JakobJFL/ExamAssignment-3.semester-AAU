using System.IO;

namespace Stregsystem
{
    public class NewStreamReader : StreamReader, IStreamReader
    {
        public NewStreamReader(string filePath) : base (filePath) { }
    }
}
