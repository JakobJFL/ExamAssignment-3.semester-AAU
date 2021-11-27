using System.Collections.Generic;

namespace Eksamensopgave
{
    public interface IFileManager
    {
        void LogTransection(IEnumerable<ITransaction> transactions);
        List<User> LoadUsers(string filePath);
        List<Product> LoadProducts(string filePath);

    }
}
