using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;

namespace DatabaseProject.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ILogger<ProductsModel> _logger;

        public ProductsModel(ILogger<ProductsModel> logger)
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
                                Product_ID = reader.GetInt32(0),
                                Product_name = reader.GetString(1),
                                Product_type = reader.GetString(2),
                                Production_date = reader.GetDateTime(3),
                                Expiration_Date = reader.GetDateTime(4),
                                Quantity_IN_Stock = reader.GetInt32(5),
                                Product_Description = reader.GetString(6),
                                Supplier_ID = reader.GetInt32(7),
                                Product_price = reader.GetInt32(8)
                            });
                        }
                    }

                    // Fetch suppliers for dropdown
                    string supplierSql = "SELECT Supplier_ID, Sup_Name FROM Supplier";
                    using (SqlCommand supplierCommand = new SqlCommand(supplierSql, connection))
                    using (SqlDataReader reader = supplierCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SupplierList.Add(new Supplier
                            {
                                Supplier_ID = reader.GetInt32(0),
                                Sup_Name = reader.GetString(1)
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

                    // Validate foreign key for supplier
                    using (SqlCommand validateCommand = new SqlCommand("SELECT COUNT(*) FROM Supplier WHERE Supplier_ID = @Supplier_ID", connection))
                    {
                        validateCommand.Parameters.AddWithValue("@Supplier_ID", NewProduct.Supplier_ID);
                        int supplierExists = (int)validateCommand.ExecuteScalar();

                        if (supplierExists == 0)
                        {
                            ModelState.AddModelError(string.Empty, "The selected supplier does not exist.");
                            return Page();
                        }
                    }

                    // Insert into Products
                    string sql = "INSERT INTO Products (Product_name, Product_type, Production_date, Expiration_Date, Quantity_IN_Stock, Product_Description, Supplier_ID, Product_price) " +
                                 "VALUES (@Product_name, @Product_type, @Production_date, @Expiration_Date, @Quantity_IN_Stock, @Product_Description, @Supplier_ID, @Product_price)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Product_name", NewProduct.Product_name);
                        command.Parameters.AddWithValue("@Product_type", NewProduct.Product_type);
                        command.Parameters.AddWithValue("@Production_date", NewProduct.Production_date);
                        command.Parameters.AddWithValue("@Expiration_Date", NewProduct.Expiration_Date);
                        command.Parameters.AddWithValue("@Quantity_IN_Stock", NewProduct.Quantity_IN_Stock);
                        command.Parameters.AddWithValue("@Product_Description", NewProduct.Product_Description);
                        command.Parameters.AddWithValue("@Supplier_ID", NewProduct.Supplier_ID);
                        command.Parameters.AddWithValue("@Product_price", NewProduct.Product_price);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting new product.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
                return Page();
            }

            return RedirectToPage();
        }
    }
}
