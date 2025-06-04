namespace DatabaseProject.Models
{
    public class LabTestCategory
    {
        public string Category { get; set; }
        public double AveragePrice { get; set; }
    }

    public class TestsByEmployee
    {
        public string EmployeeName { get; set; }
        public int TestsConducted { get; set; }
    }
}
