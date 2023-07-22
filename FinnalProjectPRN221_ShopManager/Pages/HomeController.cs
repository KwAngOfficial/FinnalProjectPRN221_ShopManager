using Microsoft.AspNetCore.Mvc;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
