using System;
using Stregsystem.Models;

namespace Stregsystem.Abstractions
{
    public interface ITransaction
    {
        public int ID { get; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string ToString();
        public void Execute();
    }
}
