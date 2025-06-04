using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseProject.Models;
using Microsoft.Data.SqlClient;

namespace DatabaseProject.Pages
{
    public class CustomerLoginModel : PageModel
    {
        [BindProperty]
        public Customerauth Customer { get; set; }
        public List<Customerauth> Customerlist { get; set; } = new List<Customerauth>();
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch all Customer data
                    string sql = "SELECT Customer_username, Customer_password FROM Customerauth";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customerlist.Add(new Customerauth
                                {
                                    customer_username = reader.GetString(0),
                                    customer_password = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Customer data: {ex.Message}");
            }

            // Redirect to Index if already logged in
            if (HttpContext.Session.GetString("IsLoggedInCustomer") == "true")
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            OnGet();

            var matchingCustomer = Customerlist.FirstOrDefault(a => a.customer_username == Customer.customer_username);

            if (matchingCustomer != null && matchingCustomer.customer_password == Customer.customer_password)
            {
                HttpContext.Session.SetString("IsLoggedInCustomer", "true"); // Set session variable
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Invalid username or password. Please try again.";
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear(); // Clear session on logout
            return RedirectToPage("/Login");
        }
    }
}
