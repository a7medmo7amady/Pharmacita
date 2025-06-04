using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DatabaseProject.Models;
using Microsoft.Data.SqlClient;

namespace DatabaseProject.Pages
{
    public class SupplierDetailsModel : PageModel
    {
        public List<Supplier> SupplierList = new List<Supplier>();
        
        [BindProperty]
        public Supplier NewSupplier { get; set; } = new Supplier();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-I1U5NEO\\SQLEXPRESS;Initial Catalog=Pharmacita;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    

                    string sql = "SELECT * FROM Supplier";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SupplierList.Add(new Supplier
                                {
                                    Supplier_ID = reader.GetInt32(0),
                                    Sup_Name = reader.GetString(1),
                                    Sup_address = reader.GetString(2),
                                    DeliveryDay = reader.GetString(3),
                                    BankName = reader.GetString(4),
                                    Accountnumber = reader.GetString(5),
                                    Payment_Terms = reader.GetString(6)
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

                    string sql = "INSERT INTO Supplier (Sup_Name, Sup_address, DeliveryDay, BankName, Accountnumber, Payment_Terms) " +
                                 "VALUES (@Sup_Name, @Sup_address, @DeliveryDay, @BankName, @Accountnumber, @Payment_Terms)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Sup_Name", NewSupplier.Sup_Name);
                        command.Parameters.AddWithValue("@Sup_address", NewSupplier.Sup_address);
                        command.Parameters.AddWithValue("@DeliveryDay", NewSupplier.DeliveryDay);
                        command.Parameters.AddWithValue("@BankName", NewSupplier.BankName);
                        command.Parameters.AddWithValue("@Accountnumber", NewSupplier.Accountnumber);
                        command.Parameters.AddWithValue("@Payment_Terms", NewSupplier.Payment_Terms);

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