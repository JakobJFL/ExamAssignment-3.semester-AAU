using System.IO;
using Stregsystem.Abstractions;

namespace Stregsystem.DataAccess
{
    public class NewStreamReader : StreamReader, IStreamReader
    {
        public NewStreamReader(string filePath) : base (filePath) { }
    }
}
