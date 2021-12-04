using System;

namespace Eksamensopgave.Models
{
    public class BuyTransaction : ITransaction
    {
        private static int _id = 1;
        public BuyTransaction(User user, Product product, decimal amount)
        {
            User = user;
            Product = product;
            Amount = amount;
            _id++;
        }
        public int ID { get; set; } = _id;
        public User User { get; set; }
        public Product Product { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public override string ToString()
        {
            return "Køb - " + ID + " | " + Amount + " | " + User.Username + " | " + Product.Name + " | " + Date;
        }
        public void Execute()
        {
            if (!Product.Active)
                throw new InvalidOperationException("Product is not active");
            else if (User.Balance >= Amount || Product.CanBeBoughtOnCredit)
                User.Balance -= Amount;
            else
                throw new InsufficientCreditsException(User.Username + " does not have enough balance to buy: " + Product.Name);
        }
    }
}
