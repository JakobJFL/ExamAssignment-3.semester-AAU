using System;

namespace Eksamensopgave
{
    class SeasonalProduct : Product
    {
        public SeasonalProduct(string name, decimal price): base(name, price)
        {
            
        }
        public override bool Active { 
            get {
                if (DateTime.Now > SeasonStartDate && DateTime.Now < SeasonEndDate)
                    return true;
                return false;
            } 
        }
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }
        public override string ToString() // Skal det her ikke også være der
        {
            return "Seasonal product " + base.ToString();
        }
    }
}
