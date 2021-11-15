using System;
using System.Collections.Generic;
using System.Linq;

namespace Eksamensopgave
{
    class Stregsystem : IStregsystem
    {
        public IEnumerable<Product> ActiveProducts { get; set; }

        public event User.UserBalanceNotification UserBalanceWarning;

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            return new InsertCashTransaction(user, amount);
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            return new BuyTransaction(user, product);
        }

        public Product GetProductByID(int id)
        {
            return ActiveProducts.First(p => p.ID == id);
        }

        public IEnumerable<ITransaction> GetTransactions(User user, int count)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
