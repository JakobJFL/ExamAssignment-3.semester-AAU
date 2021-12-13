using System;
using System.Collections.Generic;

namespace Stregsystem.Abstractions
{
    public interface ILodeFromFile<T>
    {
        public IEnumerable<T> Load(Func<string[], T> parseData);
    }
}
