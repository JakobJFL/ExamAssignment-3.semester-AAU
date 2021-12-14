namespace Stregsystem.Models
{
    public class ProductFactory
    {
        private int _id = 1;

        public Product CreateProduct(string name, decimal price)
        {
            return new Product(name, price, _id++);
        }

        public SeasonalProduct CreateSeasonalProduct(string name, decimal price)
        {
            return new SeasonalProduct(name, price, _id++);
        }
    }
}
