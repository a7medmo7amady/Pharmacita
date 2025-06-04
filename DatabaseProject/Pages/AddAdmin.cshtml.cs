using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;

namespace DatabaseProject.Pages
{
    public class AddAdminModel : PageModel
    {
        [BindProperty]
        public Adminauth Admin { get; set; }
        public IActionResult OnPost()
        {
            string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Adminauth (admin_username, admin_password) VALUES (@Username, @Password)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", Admin.admin_username);
                    command.Parameters.AddWithValue("@Password", Admin.admin_password);
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToPage("/Index");
        }
    }
}
