using System;

namespace Stregsystem.Models
{
    public class SeasonalProduct : Product
    {
        public SeasonalProduct(string name, decimal price, int id): base(name, price, id)
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
        public override string ToString() 
        {
            return "Seasonal product " + base.ToString();
        }
    }
}
