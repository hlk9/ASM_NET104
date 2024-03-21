using Microsoft.AspNetCore.Mvc;
using Org_DAL.Models;
using Org_DAL.Repository;

namespace OrganicWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDetailRepository _repos = new ProductDetailRepository();
        public IActionResult Index()
        {
            IEnumerable<ProductDetail> products = new List<ProductDetail>();
            products  = _repos.GetAll();
            return View(products);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductDetail obj)
        {
            //if(ModelState.IsValid)
           // {
                _repos.Add(obj);
                return RedirectToAction("Index");
          //  }

            //return View(obj);
        }

    }
}
