using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseProject.Models;
using Microsoft.Data.SqlClient;

namespace DatabaseProject.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Adminauth Admin { get; set; }
        public List<Adminauth> Adminlist { get; set; } = new List<Adminauth>();
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch all admin data
                    string sql = "SELECT admin_username, admin_password FROM Adminauth";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Adminlist.Add(new Adminauth
                                {
                                    admin_username = reader.GetString(0),
                                    admin_password = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching admin data: {ex.Message}");
            }

            // Redirect to Index if already logged in
            if (HttpContext.Session.GetString("IsLoggedInadmin") == "true")
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // Load admin list (necessary because OnGet doesn't populate it automatically in a POST request)
            OnGet();

            // Validate the entered credentials against the database records
            var matchingAdmin = Adminlist.FirstOrDefault(a => a.admin_username == Admin.admin_username);

            if (matchingAdmin != null && matchingAdmin.admin_password == Admin.admin_password)
            {
                HttpContext.Session.SetString("IsLoggedInadmin", "true"); // Set session variable
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
