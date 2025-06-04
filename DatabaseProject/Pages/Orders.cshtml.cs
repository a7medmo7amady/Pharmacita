using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;

namespace DatabaseProject.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly ILogger<OrdersModel> _logger;

        public OrdersModel(ILogger<OrdersModel> logger)
        {
            _logger = logger;
        }

        public List<Orders> Orderslist { get; set; } = new List<Orders>();
        public List<Customer> CustomerList { get; set; } = new List<Customer>();
        public List<Products> ProductsList { get; set; } = new List<Products>();
        public int TotalOrders { get; set; }
        [BindProperty]
        public Orders NewOrder { get; set; } = new Orders();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch orders
                    string sql = "SELECT * FROM Orders";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Orderslist.Add(new Orders
                            {
                                O_ID = reader.GetInt32(0),
                                C_ID = reader.GetInt32(1),
                                Order_Date = reader.GetDateTime(2),
                                P_ID = reader.GetInt32(3)
                            });
                        }
                    }

                    // Fetch customers for dropdown
                    string customerSql = "SELECT C_ID, C_NAME FROM Customer";
                    using (SqlCommand customerCommand = new SqlCommand(customerSql, connection))
                    using (SqlDataReader reader = customerCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerList.Add(new Customer
                            {
                                C_ID = reader.GetInt32(0),
                                C_NAME = reader.GetString(1)
                            });
                        }
                    }
                    // Fetch products for dropdown
                    string productSql = "SELECT Product_ID, Product_name FROM Products";
                    using (SqlCommand productCommand = new SqlCommand(productSql, connection))
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders or customers.");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Validate foreign key
                    using (SqlCommand validateCommand = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE C_ID = @C_ID", connection))
                    {
                        validateCommand.Parameters.AddWithValue("@C_ID", NewOrder.C_ID);
                        int customerExists = (int)validateCommand.ExecuteScalar();

                        if (customerExists == 0)
                        {
                            ModelState.AddModelError(string.Empty, "The selected customer does not exist.");
                            return Page();
                        }
                    }

                    // Insert into Orders
                    string sql = "INSERT INTO Orders (C_ID, P_ID, Order_Date) VALUES (@C_ID, @P_ID, @Order_Date)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@C_ID", NewOrder.C_ID);
                        command.Parameters.AddWithValue("@P_ID", NewOrder.P_ID);
                        command.Parameters.AddWithValue("@Order_Date", NewOrder.Order_Date);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting new order.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
                return Page();
            }

            return RedirectToPage();
        }
    }
}
