using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stregsystem.Models;
using Stregsystem.UserInterface;

namespace Stregsystem
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
            IStregsystem stregsystem = new Stregsystem(products.Where(t => t.Active == true), userFileManager.Load(ParseData.ParseUser));
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            IStregsystemController sc = new StregsystemController(stregsystem, ui);
            ui.Start();
            /*
            for (int i = 1; i < 135; i++)
            {
                Console.WriteLine(stregsystem.GetProductByID(i));
            }
            IEnumerable<User> userList = stregsystem.GetUsers(u => u.ID > 0);
            foreach (User user in userList)
            {
                Console.WriteLine(user.ToString());
            }

            stregsystem.AddCreditsToAccount(stregsystem.GetUserByUsername("rking"), 1650);
            stregsystem.BuyProduct(stregsystem.GetUserByUsername("rking"), stregsystem.GetProductByID(29));
            stregsystem.BuyProduct(stregsystem.GetUserByUsername("rking"), stregsystem.GetProductByID(29));
            */
            
        }
    }
}
