using System;
using System.Collections.Generic;

namespace Stregsystem
{
    public interface ILodeFromFile<T>
    {
        public IEnumerable<T> Load(Func<string[], T> parseData);
    }
}
