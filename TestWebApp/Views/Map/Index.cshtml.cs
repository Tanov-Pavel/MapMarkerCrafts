using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebApp.Views.Map
{
    public class IndexModel : PageModel
    {
        public string Name { get; set; } = "Pavel K.";
        public void OnGet()
        {
            this.Name = Name + " " + DateTime.Now.ToShortDateString();
        }
    }
}
