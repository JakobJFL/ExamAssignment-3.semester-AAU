using System.Collections.Generic;

namespace Eksamensopgave
{
    public interface IFileManager
    {
        void LogTransection(IEnumerable<ITransaction> transactions);
        List<User> LoadUsers();
        List<Product> LoadProducts();

    }
}
