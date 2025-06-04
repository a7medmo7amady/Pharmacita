using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;

namespace DatabaseProject.Pages
{
    public class CustomerSignUpModel : PageModel
    {
        [BindProperty]
        public Customerauth Customer { get; set; }
        public IActionResult OnPost()
        {
            string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Customerauth (customer_username, customer_password) VALUES (@Username, @Password)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", Customer.customer_username);
                    command.Parameters.AddWithValue("@Password", Customer.customer_password);
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToPage("/Index");
        }
    }
}
