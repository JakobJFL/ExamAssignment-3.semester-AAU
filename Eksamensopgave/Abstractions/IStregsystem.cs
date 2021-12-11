using System;
using System.Collections.Generic;
using Stregsystem.Models;

namespace Stregsystem
{
    public interface IStregsystem
    {
        public IEnumerable<Product> ActiveProducts { get; }
        public InsertCashTransaction AddCreditsToAccount(User user, int amount);
        public BuyTransaction BuyProduct(User user, Product product);
        public Product GetProductByID(int id);
        public IEnumerable<ITransaction> GetTransactions(User user, int count, Func<ITransaction, bool> predicate);
        public IEnumerable<User> GetUsers(Func<User, bool> predicate);
        public User GetUserByUsername(string username);
        public event UserBalanceNotification UserBalanceWarning;
        public int NotifyUserWhenBalance { get; }
    }
}
