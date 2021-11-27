using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eksamensopgave
{
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

        public List<User> LoadUsers(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                List<User> userList = new List<User>();
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    string[] firstNames = {values[1]};
                    User user = new User(firstNames, values[2], values[3], values[5])
                    {
                        Balance = decimal.Parse(values[4])
                    };
                    userList.Add(user);
                }
                return userList;
            }
        }

        public List<Product> LoadProducts(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                List<Product> productList = new List<Product>();
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');
                    string name = values[1]
                        .Replace("\"", "");
                    name = Regex.Replace(name, @"<\/?\w+>", "");
                    Product product = new Product(name, decimal.Parse(values[2])) // mangler at tilføje muligheden for at lave SeasonalProduct med deactivate_date.
                    {
                        Active = values[3] == "1" ? true : false
                    };
                    productList.Add(product);
                    
                }
                return productList;
            }
        }
    }
}
