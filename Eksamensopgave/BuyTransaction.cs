using System;

namespace Eksamensopgave
{
    class BuyTransaction : ITransaction
    {
        private static int _id = 1;
        public BuyTransaction(User user, Product product)
        {
            User = user;
            Product = product;
            _id++;
        }
        public int ID { get; set; } = _id;
        public User User { get; set; }
        public Product Product { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public override string ToString()
        {
            return "Køb - " + ID + " | " + Amount + " | " + User.Username + " | " + Product.Name + " | " + Date;
        }
        public void Execute()
        {
            if (!Product.Active)
                throw new InvalidOperationException("Product is not active");
            else if (User.Balance > Amount || Product.CanBeBoughtOnCredit)
                User.Balance -= Amount;
            else
                throw new InsufficientCreditsException(User.Username + " does not have enough balance to buy: " + Product.Name);
        }
    }
}
