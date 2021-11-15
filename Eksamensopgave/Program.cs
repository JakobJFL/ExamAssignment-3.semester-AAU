using System;
using System.Collections.Generic;

namespace Eksamensopgave
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] str = { "kat" };
            User user = new User(str, "kat", "dsf_", "eksempel2@-mit_domain.dk");
            Console.WriteLine(user.ToString());
        }
    }

    class kat : IStregsystem
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
            throw new NotImplementedException();
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
