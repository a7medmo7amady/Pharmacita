using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseProject.Models;
using Microsoft.Data.SqlClient;

namespace DatabaseProject.Pages
{
    public class EmployeeListModel : PageModel
    {
        public List<Employee> Employeelist = new List<Employee>();
        public int TotalEmployees { get; set; }
        [BindProperty]
        public Employee NewEmployee { get; set; } = new Employee();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get total user count
                    string countSql = "SELECT COUNT(*) FROM Employee";
                    using (SqlCommand countCommand = new SqlCommand(countSql, connection))
                    {
                        TotalEmployees = (int)countCommand.ExecuteScalar();
                    }

                    // Fetch all users
                    string sql = "SELECT * FROM Employee";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employeelist.Add(new Employee
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

                    string sql = "INSERT INTO Employee (E_NAME, E_Phone, E_address, E_salary, E_role, E_age, Hire_date, E_department) VALUES (@E_NAME, @E_Phone, @E_address, @E_salary, @E_role, @E_age, @Hire_date, @E_department)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@E_NAME", NewEmployee.E_NAME);
                        command.Parameters.AddWithValue("@E_Phone", NewEmployee.E_Phone);
                        command.Parameters.AddWithValue("@E_address", NewEmployee.E_address);
                        command.Parameters.AddWithValue("@E_salary", NewEmployee.E_salary);
                        command.Parameters.AddWithValue("@E_role", NewEmployee.E_role);
                        command.Parameters.AddWithValue("@E_age", NewEmployee.E_age);
                        command.Parameters.AddWithValue("@Hire_date", NewEmployee.Hire_date);
                        command.Parameters.AddWithValue("@E_department", NewEmployee.E_department);

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
