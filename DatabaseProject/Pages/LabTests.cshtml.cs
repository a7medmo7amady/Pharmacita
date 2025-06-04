using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;

namespace DatabaseProject.Pages
{
    public class LabTestsModel : PageModel
    {
        public List<LabTest> LabTestslist = new List<LabTest>();
        public int TotalLabTests { get; set; }
        [BindProperty]
        public LabTest NewLabTest { get; set; } = new LabTest();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string countSql = "SELECT COUNT(*) FROM LabTests";
                    using (SqlCommand countCommand = new SqlCommand(countSql, connection))
                    {
                        TotalLabTests = (int)countCommand.ExecuteScalar();
                    }

                    string sql = "SELECT * FROM LabTests";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                    string sql = "INSERT INTO LabTests (LT_name, LT_category, LT_price, LT_description, Result_time_frame) VALUES (@LT_name, @LT_category, @LT_price, @LT_description, @Result_time_frame)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@LT_name", NewLabTest.LT_name);
                        command.Parameters.AddWithValue("@LT_category", NewLabTest.LT_category);
                        command.Parameters.AddWithValue("@LT_price", NewLabTest.LT_price);
                        command.Parameters.AddWithValue("@LT_description", NewLabTest.LT_description);
                        command.Parameters.AddWithValue("@Result_time_frame", NewLabTest.Result_time_frame);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Page();
            }

            return RedirectToPage();
        }
    }
}