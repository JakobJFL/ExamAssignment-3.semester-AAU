using System;
using System.Collections.Generic;
using Stregsystem.Models;

namespace Stregsystem.Abstractions
{
    public interface IStregsystemHandler
    {
        IEnumerable<Product> AllProducts { get; }
        int NotifyUserWhenBalance { get; }
        List<ITransaction> Transactions { get; }
        IEnumerable<User> Users { get; }

        event UserBalanceNotification UserBalanceWarning;

        InsertCashTransaction AddCreditsToAccount(User user, int amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        IEnumerable<ITransaction> GetTransactions(User user, int count, Func<ITransaction, bool> predicate);
        User GetUserByUsername(string username);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
    }
}
