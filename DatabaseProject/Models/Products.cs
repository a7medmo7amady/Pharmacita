namespace DatabaseProject.Models
{
    public class Products
    {
        public int Product_ID { get; set; }
        public string Product_name { get; set; }
        public string Product_type { get; set; }
        public DateTime Production_date { get; set; }
        public DateTime Expiration_Date { get; set; }
        public int Quantity_IN_Stock { get; set; }
        public string Product_Description { get; set; }
        public int Supplier_ID { get; set; }
        public int Product_price { get; set; }
    }
}
