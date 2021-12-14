using System;
using Stregsystem.Abstractions;

namespace Stregsystem.Models
{
    public class InsertCashTransaction : ITransaction
    {
        public InsertCashTransaction(User user, decimal amount, int id)
        {
            ID = ID;
            User = user;
            Amount = amount;
        }
        public int ID { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public override string ToString()
        {
            return "Indbetaling - " + ID + " | " + Amount + " | " + User.Username + " | " + Date;
        }
        public void Execute()
        {
            User.Balance += Amount;
        }
    }
}
