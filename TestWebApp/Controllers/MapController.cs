using Microsoft.AspNetCore.Mvc;

namespace MapMarkerCrafts.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}