using System;
using System.Collections.Generic;
using System.Linq;

namespace Eksamensopgave
{
    class Stregsystem : IStregsystem
    {
        private readonly int _notifyUserWhenBalance = 50;

        public IEnumerable<Product> ActiveProducts { get; }
        public IEnumerable<User> Users { get; }
        public IEnumerable<ITransaction> Transactions { get; }

        public event User.UserBalanceNotification UserBalanceWarning;

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            return new InsertCashTransaction(user, amount);
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            if (user.Balance < _notifyUserWhenBalance && UserBalanceWarning != null)
                UserBalanceWarning(user, user.Balance);
            return new BuyTransaction(user, product);
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
            return (User)Users.Where(u => u.Username == username);
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            return (User)Users.Where(u => predicate(u));
        }
    }
}
