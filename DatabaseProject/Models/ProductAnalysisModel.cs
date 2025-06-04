namespace DatabaseProject.Models
{
    public class ProductStock
    {
        public string ProductType { get; set; }
        public int TotalStock { get; set; }
    }

    public class ProductPrice
    {
        public string ProductType { get; set; }
        public double AveragePrice { get; set; }
    }
}
