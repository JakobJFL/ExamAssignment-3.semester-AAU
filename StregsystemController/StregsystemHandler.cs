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
        private TransactionFactory TransactionFactory { get; } = new TransactionFactory();
        public StregsystemHandler(string productsDir, string usersDir, string logDir)
        {
            ProductFactory productFactory = new ProductFactory();
            UserFactory userFactory = new UserFactory();
            ILodeFromFile<Product> productFileManager = new LoadFromFile<Product>(new NewStreamReader(productsDir), ';');
            ILodeFromFile<User> userFileManager = new LoadFromFile<User>(new NewStreamReader(usersDir), ',');
            AllProducts = productFileManager.Load(v => ParseData.ParseProduct(v, productFactory));
            Users = userFileManager.Load(v => ParseData.ParseUser(v, userFactory));
            LogDir = logDir;
        }

        public int NotifyUserWhenBalance { get; } = 50;
        public IEnumerable<Product> AllProducts { get; }
        public IEnumerable<User> Users { get; }
        public List<ITransaction> Transactions { get; } = new List<ITransaction>();
        private string LogDir { get; }
        public event UserBalanceNotification UserBalanceWarning;

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction transaction = TransactionFactory.CreateInsertCashTransaction(user, amount);
            transaction.Execute();
            WriteToFile write = new WriteToFile(LogDir);
            write.LogTransection(transaction);
            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = TransactionFactory.CreateBuyTransaction(user, product, product.Price);
            transaction.Execute();
            WriteToFile write = new WriteToFile(LogDir);
            Transactions.Add(transaction);
            write.LogTransection(transaction);
            if (user.Balance < NotifyUserWhenBalance && UserBalanceWarning != null)
                UserBalanceWarning(user, user.Balance);
            return transaction;
        }

        public Product GetProductByID(int id)
        {
            return AllProducts.FirstOrDefault(p => p.ID == id) ?? throw new ProductNotFoundException();
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
