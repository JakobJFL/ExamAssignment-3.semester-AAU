using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Eksamensopgave
{
    class Program
    {
        private static readonly string _productsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "products.csv");
        private static readonly string _usersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "users.csv");
        static void Main(string[] args)
        {
            IFileManager<Product> fileManager = new FileManager<Product>(new MyStreamReader(_productsFilePath), ';');
            IStregsystem stregsystem = new Stregsystem(fileManager.Load(values =>
            {
                string name = values[1].Replace("\"", "");
                name = Regex.Replace(name, @"<\/?\w+>", "");
                Product product = new Product(name, decimal.Parse(values[2])) // mangler at tilføje muligheden for at lave SeasonalProduct med deactivate_date.
                {
                    Active = values[3] == "1" ? true : false
                };
                return product;
            }));
            for (int i = 1; i < 135; i++)
            {
                Console.WriteLine(stregsystem.GetProductByID(i));
            }

        }
    }
}
