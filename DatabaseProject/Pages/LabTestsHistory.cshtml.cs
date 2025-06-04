using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;


namespace DatabaseProject.Pages
{
    public class LabTestsHistoryModel : PageModel
    {
        private readonly ILogger<LabTestsHistoryModel> _logger;

        public LabTestsHistoryModel(ILogger<LabTestsHistoryModel> logger)
        {
            _logger = logger;
        }

        public List<Make> LabTestsRequestslist { get; set; } = new List<Make>();
        public List<LabTest> LabTestsList { get; set; } = new List<LabTest>();
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

                    // Getting requests made bY that Customer
                    string orderSql = "SELECT * FROM Make WHERE C_ID = @C_ID";
                    using (SqlCommand orderCommand = new SqlCommand(orderSql, connection))
                    {
                        orderCommand.Parameters.AddWithValue("@C_ID", id);
                        using (SqlDataReader reader = orderCommand.ExecuteReader())
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
                    }
                    // Getting Customers
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
                    // Getting lab tests
                    string labtestsSql = "SELECT LT_ID, LT_name FROM LabTests";
                    using (SqlCommand productCommand = new SqlCommand(labtestsSql, connection))
                    {
                        using (SqlDataReader reader = productCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LabTestsList.Add(new LabTest
                                {
                                    LT_ID = reader.GetInt32(0),
                                    LT_name = reader.GetString(1)
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