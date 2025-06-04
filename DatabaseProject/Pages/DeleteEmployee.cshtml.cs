using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;



namespace DatabaseProject.Pages
{
    public class DeleteEmployeeModel : PageModel
    {
        [BindProperty]
        public Employee Employee { get; set; } = new Employee();

        public void OnGet(int id)
        {
            string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Employee WHERE E_ID = @ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Employee = new Employee
                            {
                                E_ID = reader.GetInt32(0),
                                E_NAME = reader.GetString(1),
                                E_Phone = reader.GetString(2),
                                E_address = reader.GetString(3),
                                E_salary = reader.GetInt32(4),
                                E_role = reader.GetString(5),
                                E_age = reader.GetInt32(6),
                                Hire_date = reader.GetDateTime(7),
                                E_department = reader.GetString(8)
                            };
                        }
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Employee WHERE E_ID = @ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", Employee.E_ID);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/EmployeeList");
        }
    }
}
