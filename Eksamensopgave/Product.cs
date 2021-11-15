namespace Eksamensopgave
{
    class Product
    {
        private static int _id = 1;
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
            _id++;
        }
        public int ID { get; } = _id;
        public string Name { get; private set; }
        public decimal Price { get; set; }
        public bool Active { get; set; } = true;
        public bool CanBeBoughtOnCredit { get; set; } = false;
        public override string ToString()
        {
            return ID + " | " + Name + " | " + Price;
        }
    }
}
