using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave
{
    public interface IFileManager
    {
        void LogTransection(IEnumerable<ITransaction> transactions);
    }
    public class FileManager : IFileManager
    {
        public async void LogTransection(IEnumerable<ITransaction> transactions)
        {
            string csvData = "ID;User;Amount;Date\n";
            foreach (ITransaction t in transactions)
            {
                csvData += t.ID + ";";
                csvData += t.User.ToString() + ";";
                csvData += t.Amount + ";";
                csvData += t.Date + ";\n";
            }
            await File.WriteAllTextAsync("LogData.csv", csvData);
        }
    }
}
