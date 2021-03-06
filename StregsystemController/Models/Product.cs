namespace Stregsystem.Models
{
    public class Product
    {
        public Product(string name, decimal price, int id)
        {
            ID = id;
            Name = name;
            Price = price;
        }
        public int ID { get; }
        public string Name { get; }
        public decimal Price { get; }
        public virtual bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; } = false;
        public override string ToString()
        {
            return ID + "\t| " + Name + " | " + Price + " kr.";
        }
    }
}
