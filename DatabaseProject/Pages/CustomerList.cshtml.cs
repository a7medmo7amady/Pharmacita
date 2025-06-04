using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using DatabaseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseProject.Pages
{
    public class CustomerListModel : PageModel
    {
        public List<Customer> Customerlist = new List<Customer>();
        public int TotalCustomers { get; set; }
        [BindProperty]
        public Customer NewCustomer { get; set; } = new Customer();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get total user count
                    string countSql = "SELECT COUNT(*) FROM Customer";
                    using (SqlCommand countCommand = new SqlCommand(countSql, connection))
                    {
                        TotalCustomers = (int)countCommand.ExecuteScalar();
                    }

                    // Fetch all users
                    string sql = "SELECT * FROM Customer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customerlist.Add(new Customer
                                {
                                    C_ID = reader.GetInt32(0),
                                    C_NAME = reader.GetString(1),
                                    C_Phone = reader.GetString(2),
                                    C_Address = reader.GetString(3),
                                    PAYMENT_INFO = reader.GetString(4),
                                    C_age = reader.GetInt32(5)
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

                    string sql = "INSERT INTO Customer (C_NAME, C_Phone, C_Address, PAYMENT_INFO, C_age) VALUES (@C_NAME, @C_Phone, @C_Address, @PAYMENT_INFO, @C_age)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@C_NAME", NewCustomer.C_NAME);
                        command.Parameters.AddWithValue("@C_Phone", NewCustomer.C_Phone);
                        command.Parameters.AddWithValue("@C_Address", NewCustomer.C_Address);
                        command.Parameters.AddWithValue("@PAYMENT_INFO", NewCustomer.PAYMENT_INFO);
                        command.Parameters.AddWithValue("@C_age", NewCustomer.C_age);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Page();
            }

            return RedirectToPage(); // Refresh to show updated list
        }

    }
}
