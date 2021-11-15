using System;

namespace Eksamensopgave
{
    class SeasonalProduct
    {
        private static int _id = 1;
        public SeasonalProduct(string name, string price)
        {
            Name = name;
            Price = price;
            _id++;
        }
        public int ID { get; } = _id;
        public string Name { get; private set; }
        public string Price { get; set; }
        public bool Active { 
            get {
                if (DateTime.Now > SeasonStartDate && DateTime.Now < SeasonEndDate)
                    return true;
                return false;
            } 
        }
        public bool CanBeBoughtOnCredit { get; set; } = false;
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }
        public override string ToString() // Skal det her ikke også være der
        {
            return ID + " | " + Name + " | " + Price;
        }
    }
}
