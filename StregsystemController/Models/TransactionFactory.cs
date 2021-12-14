using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stregsystem.Abstractions;

namespace Stregsystem.Models
{
    public class TransactionFactory
    {
        private int _id = 1;
        public BuyTransaction CreateBuyTransaction(User user, Product product, decimal amount)
        {
            return new BuyTransaction(user, product, amount, _id++);
        }
        public InsertCashTransaction CreateInsertCashTransaction(User user, decimal amount)
        {
            return new InsertCashTransaction(user, amount, _id++);
        }
    }

}
