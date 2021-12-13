using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stregsystem;
using Stregsystem.Abstractions;
using Stregsystem.DataAccess;
using Stregsystem.Models;
using Stregsystem.UserInterface;

namespace StregsystemController
{
    public class Program
    {
        private static readonly string _productsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "products.csv");
        private static readonly string _usersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "users.csv");
        private static void Main(string[] args)
        {
            ILodeFromFile<Product> productFileManager = new LodeFromFile<Product>(new NewStreamReader(_productsFilePath), ';');
            ILodeFromFile<User> userFileManager = new LodeFromFile<User>(new NewStreamReader(_usersFilePath), ',');
            IEnumerable<Product> products = productFileManager.Load(ParseData.ParseProduct);
            IStregsystemHandler stregsystemHandler = new StregsystemHandler(products.Where(t => t.Active == true), userFileManager.Load(ParseData.ParseUser));
            IStregsystemUI ui = new StregsystemCLI(stregsystemHandler);
            IStregsystemCommandParser sc = new StregsystemCommandParser(stregsystemHandler, ui);
            ui.Start();
            
        }
    }
}
