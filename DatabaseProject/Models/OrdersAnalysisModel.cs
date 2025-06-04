namespace DatabaseProject.Models
{
    public class MonthlyOrders
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalOrders { get; set; }
    }

    public class Revenue
    {
        public double TotalRevenue { get; set; }
    }
}
