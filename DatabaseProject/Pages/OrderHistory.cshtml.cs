using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;


namespace DatabaseProject.Pages
{
    public class OrderHistoryModel : PageModel
    {
        private readonly ILogger<OrderHistoryModel> _logger;

        public OrderHistoryModel(ILogger<OrderHistoryModel> logger)
        {
            _logger = logger;
        }

        public List<Orders> OrdersList { get; set; } = new List<Orders>();
        public List<Products> ProductsList { get; set; } = new List<Products>();
        [BindProperty]
        public Customer selectedCustomer { get; set; }

        public void OnGet(int id)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Getting Orders made bY that Customer
                    string orderSql = "SELECT * FROM Orders WHERE C_ID = @C_ID";
                    using (SqlCommand orderCommand = new SqlCommand(orderSql, connection))
                    {
                        orderCommand.Parameters.AddWithValue("@C_ID", id);
                        using (SqlDataReader reader = orderCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrdersList.Add(new Orders
                                {
                                    O_ID = reader.GetInt32(0),
                                    C_ID = reader.GetInt32(1),
                                    Order_Date = reader.GetDateTime(2),
                                    P_ID = reader.GetInt32(3)
                                });
                            }
                        }
                    }
                    string customerSql = "SELECT C_ID, C_NAME FROM Customer WHERE C_ID = @C_ID";
                    using (SqlCommand customerCommand = new SqlCommand(customerSql, connection))
                    {
                        customerCommand.Parameters.AddWithValue("@C_ID", id);
                        using (SqlDataReader reader = customerCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedCustomer = new Customer
                                {
                                    C_ID = reader.GetInt32(0),
                                    C_NAME = reader.GetString(1)
                                };
                            }
                        }
                    }
                    string productSql = "SELECT Product_ID, Product_name FROM Products";
                    using (SqlCommand productCommand = new SqlCommand(productSql, connection))
                    {
                        using (SqlDataReader reader = productCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductsList.Add(new Products
                                {
                                    Product_ID = reader.GetInt32(0),
                                    Product_name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
            }
        }
    }

}