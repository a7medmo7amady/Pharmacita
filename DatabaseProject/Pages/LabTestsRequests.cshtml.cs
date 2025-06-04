using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;

namespace DatabaseProject.Pages
{
    public class LabTestsRequestsModel : PageModel
    {
        private readonly ILogger<LabTestsRequestsModel> _logger;

        public LabTestsRequestsModel(ILogger<LabTestsRequestsModel> logger)
        {
            _logger = logger;
        }

        public List<Make> LabTestsRequestslist { get; set; } = new List<Make>();
        public List<LabTest> LabTestslist { get; set; } = new List<LabTest>();

        public List<Customer> CustomerList { get; set; } = new List<Customer>();
        public int TotalOrders { get; set; }
        [BindProperty]
        public Make NewLabTestRequest { get; set; } = new Make();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Fetch LabTestsRequests
                    string sql = "SELECT * FROM Make";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LabTestsRequestslist.Add(new Make
                            {
                                LT_ID = reader.GetInt32(0),
                                C_ID = reader.GetInt32(1),
                                Test_Date = reader.GetDateTime(2)                               
                            });
                        }
                    }

                    // Fetch LabTests
                    string LabTestsql = "SELECT * FROM LabTests";
                    using (SqlCommand command = new SqlCommand(LabTestsql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LabTestslist.Add(new LabTest
                            {
                                LT_ID = reader.GetInt32(0),
                                LT_name = reader.GetString(1),
                                LT_category = reader.GetString(2),
                                LT_price = reader.GetInt32(3),
                                LT_description = reader.GetString(4),
                                Result_time_frame = reader.GetString(5)
                            });
                        }
                    }

                    // Fetch customers
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

                    // Validate Customer
                    using (SqlCommand validateCommand = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE C_ID = @C_ID", connection))
                    {
                        validateCommand.Parameters.AddWithValue("@C_ID", NewLabTestRequest.C_ID);
                        int customerExists = (int)validateCommand.ExecuteScalar();

                        if (customerExists == 0)
                        {
                            ModelState.AddModelError(string.Empty, "The selected customer does not exist.");
                            return Page();
                        }
                    }

                    // Insert into Orders
                    string sql = "INSERT INTO Make (LT_ID, C_ID, Test_Date) VALUES (@LT_ID, @C_ID, @Test_Date)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@LT_ID", NewLabTestRequest.LT_ID);
                        command.Parameters.AddWithValue("@C_ID", NewLabTestRequest.C_ID);
                        command.Parameters.AddWithValue("@Test_Date", NewLabTestRequest.Test_Date);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting new Lab Test Request.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
                return Page();
            }

            return RedirectToPage();
        }
    }
}
