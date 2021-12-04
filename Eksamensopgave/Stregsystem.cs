using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eksamensopgave.Models;

namespace Eksamensopgave
{
    public class Stregsystem : IStregsystem
    {
        private static readonly string _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.csv");
        public Stregsystem(IEnumerable<Product> activeProducts, IEnumerable<User> users)
        {
            ActiveProducts = activeProducts;
            Users = users;
        }
        private readonly int _notifyUserWhenBalance = 50;

        public IEnumerable<Product> ActiveProducts { get; }
        public IEnumerable<User> Users { get; }
        public IEnumerable<ITransaction> Transactions { get; }

        public event User.UserBalanceNotification UserBalanceWarning;

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            transaction.Execute();
            WriteToFile write = new WriteToFile(_logFilePath);
            write.LogTransection(transaction);
            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            if (user.Balance < _notifyUserWhenBalance && UserBalanceWarning != null)
                UserBalanceWarning(user, user.Balance);
            BuyTransaction transaction = new BuyTransaction(user, product, product.Price);
            try
            {
                transaction.Execute();
                WriteToFile write = new WriteToFile(_logFilePath);
                write.LogTransection(transaction);
            }
            catch (InsufficientCreditsException ex)
            {
                Console.WriteLine(ex.Message); //skal nok være et andet sted
            }
            return transaction;
        }

        public Product GetProductByID(int id)
        {
            return ActiveProducts.FirstOrDefault(p => p.ID == id) ?? throw new Exception(); // Måske noget andet end Exception kan også bare return n
        }

        public IEnumerable<ITransaction> GetTransactions(User user, int count)
        {
            return Transactions.Where(u => u == user).Take(count);
        }

        public User GetUserByUsername(string username)
        {
            return (User)Users.Where(u => u.Username == username).First();
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return Users.Where(u => predicate(u)).ToList();
        }
    }
}
