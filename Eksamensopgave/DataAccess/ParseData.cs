using System.Text.RegularExpressions;
using Eksamensopgave.Models;

namespace Eksamensopgave
{
    public class ParseData
    {
        static public Product ParseProduct(string[] values)
        {
            string name = values[1].Replace("\"", "");
            name = Regex.Replace(name, @"<\/? ?\w+ ?>", "");
            Product product = new Product(name, decimal.Parse(values[2]))
            {
                Active = (values[3] == "1" ? true : false)
            };
            return product;
        }
        static public User ParseUser(string[] values)
        {
            string[] firstNames = { values[1] };
            User user = new User(firstNames, values[2], values[3], values[5])
            {
                Balance = decimal.Parse(values[4])
            };
            return user;
        }
    }
}
