 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stregsystem.Abstractions;
using Stregsystem.DataAccess;
using Stregsystem.Exceptions;
using Stregsystem.Models;

namespace Stregsystem
{
    public class StregsystemHandler : IStregsystemHandler
    {
        private static readonly string _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.csv");
        public StregsystemHandler(IEnumerable<Product> activeProducts, IEnumerable<User> users)
        {
            ActiveProducts = activeProducts;
            Users = users;
        }
        public int NotifyUserWhenBalance { get; } = 50;
        public IEnumerable<Product> ActiveProducts { get; }
        public IEnumerable<User> Users { get; }
        public List<ITransaction> Transactions { get; } = new List<ITransaction>();

        public event UserBalanceNotification UserBalanceWarning;

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
            BuyTransaction transaction = new BuyTransaction(user, product, product.Price);
            transaction.Execute();
            WriteToFile write = new WriteToFile(_logFilePath);
            Transactions.Add(transaction);
            write.LogTransection(transaction);
            if (user.Balance < NotifyUserWhenBalance && UserBalanceWarning != null)
                UserBalanceWarning(user, user.Balance);
            return transaction;
        }
        public Product GetProductByID(int id)
        {
            return ActiveProducts.FirstOrDefault(p => p.ID == id) ?? throw new ProductNotFoundException(); 
        }

        public IEnumerable<ITransaction> GetTransactions(User user, int count, Func<ITransaction, bool> predicate)
        {
            if (Transactions == null)
                throw new NoTransactionsFound();
            return Transactions
                .Where(t => t.User == user)
                .Where(predicate)
                .OrderByDescending(t => t.ID)
                .Take(count);
        }

        public User GetUserByUsername(string username)
        {
            return Users.FirstOrDefault(u => u.Username == username) ?? throw new UserNotFoundException();
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return Users.Where(u => predicate(u)).ToList();
        }
    }
}
