using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Stregsystem.Abstractions;
using Stregsystem.Models;

namespace Stregsystem.DataAccess
{
    public class LoadFromFile<T> : ILodeFromFile<T>
    {
        public IStreamReader Reader { get; }
        public char SplitChar { get; }

        public LoadFromFile(IStreamReader reader, char splitChar)
        {
            Reader = reader;
            SplitChar = splitChar;
        }
        public IEnumerable<T> Load(Func<string[], T> parseData)
        {
            List<T> result = new List<T>();
            Reader.ReadLine(); // Skip header
            while (!Reader.EndOfStream)
            {
                string line = Reader.ReadLine();
                string[] values = line.Split(SplitChar);
                T dataObj = parseData(values);
                result.Add(dataObj);
            }
            return result;
        }
    }
}
