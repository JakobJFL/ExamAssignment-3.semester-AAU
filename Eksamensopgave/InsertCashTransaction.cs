﻿using System;

namespace Eksamensopgave
{
    class InsertCashTransaction : ITransaction
    {
        private static int _id = 1;
        public InsertCashTransaction(User user, int amount)
        {
            User = user;
            Amount = amount;
            _id++;
        }
        public int ID { get; set; } = _id;
        public User User { get; set; }
        public DateTime Date { get; set; }
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
