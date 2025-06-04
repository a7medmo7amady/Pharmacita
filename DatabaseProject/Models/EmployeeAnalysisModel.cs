namespace DatabaseProject.Models
{
    public class EmployeeSalary
    {
        public string Role { get; set; }
        public double AverageSalary { get; set; }
    }

    public class EmployeeHireStats
    {
        public int HireYear { get; set; }
        public int EmployeeCount { get; set; }
    }
}
