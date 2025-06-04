using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;

namespace DatabaseProject.Pages
{
    public class CustomerProductsModel : PageModel
    {
        private readonly ILogger<CustomerProductsModel> _logger;

        public CustomerProductsModel(ILogger<CustomerProductsModel> logger)
        {
            _logger = logger;
        }

        public List<Products> ProductList { get; set; } = new List<Products>();
        public List<Supplier> SupplierList { get; set; } = new List<Supplier>();
        public int TotalProducts { get; set; }
        [BindProperty]
        public Products NewProduct { get; set; } = new Products();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch products
                    string sql = "SELECT * FROM Products";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductList.Add(new Products
                            {
                                Product_name = reader.GetString(1),
                                Product_type = reader.GetString(2),
                                Production_date = reader.GetDateTime(3),
                                Expiration_Date = reader.GetDateTime(4),
                                Product_Description = reader.GetString(6),
                                Product_price = reader.GetInt32(8)
                            });
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products or suppliers.");
            }
        }

      
    }
}
