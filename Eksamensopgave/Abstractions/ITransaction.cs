using System;
using Eksamensopgave.Models;

namespace Eksamensopgave
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
