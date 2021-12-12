using System.Text.RegularExpressions;
using Stregsystem.Models;

namespace Stregsystem
{
    public class ParseData
    {
        static private int _divideToGetDecimal = 100;
        static public Product ParseProduct(string[] values)
        {
            string name = values[1].Replace("\"", "");
            name = Regex.Replace(name, @"<\/? ?\w+ ?>", "");
            decimal price = decimal.Parse(values[2]) / _divideToGetDecimal;
            Product product = new Product(name, price)
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
                Balance = decimal.Parse(values[4]) / _divideToGetDecimal
            };
            return user;
        }
    }
}
