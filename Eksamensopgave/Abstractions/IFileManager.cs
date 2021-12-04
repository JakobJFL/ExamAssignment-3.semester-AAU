using System;
using System.Collections.Generic;

namespace Eksamensopgave
{
    public interface IFileManager<T>
    {
        public void LogTransection(IEnumerable<ITransaction> transactions);
        public List<User> LoadUsers(string filePath);
        public List<Product> LoadProducts(string filePath);
        public IEnumerable<T> Load(Func<string[], T> parseData);

    }
}
