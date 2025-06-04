using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using System.Collections.Generic;
using System;

namespace DatabaseProject.Pages
{
    public class AdminResetPasswordModel : PageModel
    {
        [BindProperty]
        public Adminauth Admin { get; set; }

        [BindProperty]
        public string new_password { get; set; }  

        public string Message { get; set; }

        public IActionResult OnPost()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Adminauth SET admin_password = @Password WHERE admin_username = @Username";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Username", Admin.admin_username);
                        command.Parameters.AddWithValue("@Password", new_password);  
                        int rows = command.ExecuteNonQuery();
                        Message = rows > 0 ? "Password reset successful." : "Admin not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "Error: " + ex.Message;
            }
            return RedirectToPage("/Index");
        }
    }
}
