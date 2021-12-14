using System.Text.RegularExpressions;
using Stregsystem.Models;

namespace Stregsystem.DataAccess
{
    public class ParseData
    {
        private static readonly int _divideToGetDecimal = 100;
        public static Product ParseProduct(string[] values, ProductFactory factory)
        {
            string name = values[1].Replace("\"", "");
            name = Regex.Replace(name, @"<\/? ?\w+ ?>", "");
            decimal price = decimal.Parse(values[2]) / _divideToGetDecimal;
            Product product = factory.CreateProduct(name, price);
            product.Active = values[3] == "1";
            return product;
        }
        public static User ParseUser(string[] values, UserFactory factory)
        {
            string[] firstNames = { values[1] };
            User user = factory.CreateUser(firstNames, values[2], values[3], values[5]);
            user.Balance = decimal.Parse(values[4]) / _divideToGetDecimal;
            return user;
        }
    }
}
