using Microsoft.AspNetCore.Mvc;

namespace OrganicWebApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
