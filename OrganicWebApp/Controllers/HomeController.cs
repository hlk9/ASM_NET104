using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Org_DAL.Models;
using Org_DAL.Repository;
using OrganicWebApp.Models;
using System.Diagnostics;

namespace OrganicWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryRepository _categoryRepository = new CategoryRepository();
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {

            string lstRaw = "";

            if (Request.Cookies.TryGetValue("cartData", out lstRaw))
            {
                List<CartItem> cartItems = new List<CartItem>();
                cartItems = JsonConvert.DeserializeObject<List<CartItem>>(lstRaw);
                HttpContext.Session.SetInt32("TotalInCart", cartItems.Count);



                string a = HttpContext.Session.GetString("TotalInCart");
                string cookie = _httpContextAccessor.HttpContext.Request.Cookies["cartData"];

            }
            else
            {
                List<CartItem> listCartItem = new List<CartItem>();
                string rawJson = JsonConvert.SerializeObject(listCartItem);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMonths(1);
                options.IsEssential = true;
                options.Path = "/";
                Response.Cookies.Append("cartData", rawJson, options);
            }
            List<Category> categories = _categoryRepository.GetAll().ToList();
            ViewBag.ListCategory = new SelectList(categories, "Id", "CategoryName");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
