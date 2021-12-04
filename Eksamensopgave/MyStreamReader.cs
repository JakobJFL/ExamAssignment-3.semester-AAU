using System.IO;

namespace Eksamensopgave
{
    public class MyStreamReader : StreamReader, IStreamReader
    {
        public MyStreamReader(string filePath) : base (filePath)
        {

        }
    }
}
