using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DatabaseProject.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedInadmin") != "true" &&
                HttpContext.Session.GetString("IsLoggedInCustomer") != "true")
            {
                Response.Redirect("/ChooseLogin");
            }
        }
    }
}
