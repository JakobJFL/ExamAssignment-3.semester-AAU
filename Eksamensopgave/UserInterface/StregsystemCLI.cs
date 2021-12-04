using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eksamensopgave.Models;

namespace Eksamensopgave.UserInterface
{
    public class StregsystemCLI : IStregsystemUI
    {
        public delegate void StregsystemEvent(string command);

        public event StregsystemEvent CommandEntered;

        private readonly ConsoleColor _defaultConsoleColor = ConsoleColor.White;
        public IStregsystem Stregsystem { get; }
        public StregsystemCLI(IStregsystem stregsystem)
        {
            Stregsystem = stregsystem;
        }

        public void Start()
        {
            Console.WriteLine("============= Produkter =============");
            Console.WriteLine(" - ID|Navn|Pris -");
            foreach (Product product in Stregsystem.ActiveProducts)
            {
                Console.WriteLine("  - " + product.ToString());
            }
            Console.WriteLine("==========================");
            //CommandEntered += noget;
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\""+transaction.User.Username + "\" har købt: \"" + transaction.Product.Name + "\" for " + transaction.Amount + "kr.");
            Console.ForegroundColor = _defaultConsoleColor;
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\"" + transaction.User.Username + "\" har købt " + count + " af: \"" + transaction.Product.Name + "\" for " + transaction.Amount + "kr.");
            Console.ForegroundColor = _defaultConsoleColor;
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user.ToString());
        }
        public void Close()
        {
            throw new NotImplementedException();
        }
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            DisplayGeneralError("Administratorkommando \"" + adminCommand + "\" er ikke fundet");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            DisplayGeneralError("\"" + user.Username + "\" har ikke råd til: \"" + product.Name + "\"");
        }

        public void DisplayProductNotFound(string product)
        {
            DisplayGeneralError("Produktet: \"" + product + "\" kunne ikke findes");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            DisplayGeneralError("Kommandoen: \"" + command + "\" var ikke indtastet korrekt");
        }

        public void DisplayUserNotFound(string username)
        {
            DisplayGeneralError("Brugeren: \"" + username + "\" kunne ikke findes");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorString);
            Console.ForegroundColor = _defaultConsoleColor;
        }
    }
}
