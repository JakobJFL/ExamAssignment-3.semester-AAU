using System.IO;
using Stregsystem.Abstractions;
using Stregsystem.Models;

namespace Stregsystem.DataAccess
{
    public class WriteToFile
    {
        public WriteToFile(string path)
        {
            Path = path;
        }
        private string Path { get; }
        private async void CreateLogFile()
        {
            string csvData = "Transection type;ID;User;Amount;Date\n";
            await File.WriteAllTextAsync(Path, csvData);
        }
        public async void LogTransection(ITransaction transaction)
        {
            if (!File.Exists(Path))
                CreateLogFile();
            string csvData = "";
            if (transaction is BuyTransaction)
                csvData += "BuyTransaction;";
            else
                csvData += "InsertCashTransaction;";
            csvData += transaction.ID + ";";
            csvData += transaction.User.ToString() + ";";
            csvData += transaction.Amount + ";";
            csvData += transaction.Date + ";\n";
            await File.AppendAllTextAsync(Path, csvData);
        }
    }
}
