using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MapMarkerCrafts.Map
{
    public class IndexModel : PageModel
    {
        public string Name { get; set; }
        public void OnGet()
        {
            Name = "Hello";
        }
    }
}
