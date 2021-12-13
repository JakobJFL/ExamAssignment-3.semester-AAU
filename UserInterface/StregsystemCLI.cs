using System;
using System.Collections.Generic;
using System.Linq;
using Stregsystem.Abstractions;
using Stregsystem.Models;
using UserInterface.Abstractions;

namespace UserInterface
{
    public class StregsystemCLI : IStregsystemUI
    {
        public delegate void StregsystemEvent(string command);

        public event StregsystemEvent CommandEntered;

        private readonly ConsoleColor _defaultConsoleColor = ConsoleColor.White;
        private IStregsystemHandler Stregsystem { get; }
        public StregsystemCLI(IStregsystemHandler stregsystem)
        {
            Stregsystem = stregsystem;
            Stregsystem.UserBalanceWarning += DisplayUserBalanceWarning;
        }

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============= Produkter =============");
            Console.WriteLine(" - ID|Navn|Pris -");
            Console.ForegroundColor = _defaultConsoleColor;
            foreach (Product product in Stregsystem.AllProducts)
            {
                if (product.Active)
                    Console.WriteLine("  - " + product.ToString());
            }
            ListenForConsoleInput();
        }

        public void ListenForConsoleInput()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==========================");
            Console.ForegroundColor = _defaultConsoleColor;
            string command = Console.ReadLine();
            CommandEntered(command);
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(transaction.User.Username + " har købt: \"" + transaction.Product.Name + "\" for " + transaction.Amount + " kr.");
            Console.ForegroundColor = _defaultConsoleColor;
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(transaction.User.Username + " har købt " + count + " af: \"" + transaction.Product.Name + "\" for " + transaction.Amount * count + " kr.");
            Console.ForegroundColor = _defaultConsoleColor;
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user.ToString());
            List<ITransaction> transactionsList = Stregsystem.GetTransactions(user, 10, t => t is BuyTransaction).ToList();
            if (user.Balance < Stregsystem.NotifyUserWhenBalance)
                DisplayUserBalanceWarning(user, user.Balance);
            if (transactionsList.Count != 0)
            {
                Console.WriteLine("============= Tidligere køb =============");
                foreach (ITransaction tran in transactionsList)
                    Console.WriteLine(((BuyTransaction)tran).ToString());
            }
        }
        public void Close()
        {
            Environment.Exit(0);
        }
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            DisplayGeneralError("Administratorkommando \"" + adminCommand + "\" er ikke fundet");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            DisplayGeneralError(user.Username + " har ikke råd til: \"" + product.Name + "\"");
        }

        public void DisplayProductNotFound(string product)
        {
            DisplayGeneralError("Produktet med Id: \"" + product + "\" kunne ikke findes");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            DisplayGeneralError("Kommandoen: \"" + command + "\" har for mange argumenter");
        }
        public void DisplayFormatError(string command)
        {
            DisplayGeneralError("Kommandoen: \"" + command + "\" var ikke formateret korrekt");
        }

        public void DisplayCommandNotFoundError(string command)
        {
            DisplayGeneralError("Kommandoen: \"" + command + "\" kunne ikke findes");
        }

        public void DisplayUserNotFound(string username)
        {
            DisplayGeneralError("Brugeren med brugernavnet: \"" + username + "\" kunne ikke findes");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorString);
            Console.ForegroundColor = _defaultConsoleColor;
        }

        public void DisplayUserBalanceWarning(User user, decimal balance)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(user.Username + "s er salto kun på: " + balance + " kr.");
            Console.ForegroundColor = _defaultConsoleColor;
        }
    }
}
