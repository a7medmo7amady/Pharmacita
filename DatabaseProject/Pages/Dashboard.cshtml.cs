using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using DatabaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DatabaseProject.Pages
{
    public class DashboardModel : PageModel
    {
        public List<CustomerDemographics> CustomerDemographics { get; set; } = new List<CustomerDemographics>();
        public List<EmployeeSalary> EmployeeSalaries { get; set; } = new List<EmployeeSalary>();
        public List<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
        public List<MonthlyOrders> MonthlyOrders { get; set; } = new List<MonthlyOrders>();
        public List<LabTestCategory> LabTestCategories { get; set; } = new List<LabTestCategory>();

        public void OnGet()
        {
            string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Fetch customer demographics
                string customerQuery = "SELECT PAYMENT_INFO, COUNT(*) AS Customer_Count FROM Customer GROUP BY PAYMENT_INFO";
                using (SqlCommand command = new SqlCommand(customerQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerDemographics.Add(new CustomerDemographics
                        {
                            PaymentInfo = reader.GetString(0),
                            CustomerCount = reader.GetInt32(1)
                        });
                    }
                }

                // Fetch employee salaries
                string employeeQuery = "SELECT E_role, AVG(E_salary) AS Avg_Salary FROM Employee GROUP BY E_role";
                using (SqlCommand command = new SqlCommand(employeeQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EmployeeSalaries.Add(new EmployeeSalary
                        {
                            Role = reader.GetString(0),
                            AverageSalary = reader.IsDBNull(1) ? 0.0 : Convert.ToDouble(reader.GetValue(1)) // General handling for numeric types
                        });
                    }
                }

                // Fetch product stocks
                string productStockQuery = "SELECT Product_type, SUM(Quantity_IN_Stock) AS Total_Stock FROM Products GROUP BY Product_type";
                using (SqlCommand command = new SqlCommand(productStockQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductStocks.Add(new ProductStock
                        {
                            ProductType = reader.GetString(0),
                            TotalStock = reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                        });
                    }
                }

                // Fetch monthly orders
                string monthlyOrdersQuery = @"
                    SELECT MONTH(Order_Date) AS Month, YEAR(Order_Date) AS Year, COUNT(*) AS Total_Orders
                    FROM Orders
                    GROUP BY YEAR(Order_Date), MONTH(Order_Date)";
                using (SqlCommand command = new SqlCommand(monthlyOrdersQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MonthlyOrders.Add(new MonthlyOrders
                        {
                            Month = reader.GetInt32(0),
                            Year = reader.GetInt32(1),
                            TotalOrders = reader.GetInt32(2)
                        });
                    }
                }

                // Fetch lab test categories
                string labTestQuery = "SELECT LT_category, AVG(LT_price) AS Avg_Price FROM LabTests GROUP BY LT_category";
                using (SqlCommand command = new SqlCommand(labTestQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LabTestCategories.Add(new LabTestCategory
                        {
                            Category = reader.GetString(0),
                            AveragePrice = reader.IsDBNull(1) ? 0.0 : Convert.ToDouble(reader.GetValue(1))
                        });
                    }
                }

                
            }
        }
    }
}
