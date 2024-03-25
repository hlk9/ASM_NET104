using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org_DAL.Models;
using Org_DAL.Repository;

namespace OrganicWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repos = new ProductRepository();
        public readonly CategoryRepository _categories = new CategoryRepository();
        ProductRepository _products = new ProductRepository();
        public IActionResult Index()
        {
            IEnumerable<Product> products = new List<Product>();
            products = _repos.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            var listCate = _categories.GetAll();
            ViewBag.CategoryList = new SelectList(listCate, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj, IFormFile imageFile)
        {
            //if(ModelState.IsValid)
            // {
            if (imageFile != null && imageFile.Length > 0)
            {
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Products");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var imagePath = Path.Combine(directoryPath, imageFile.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                obj.ImageUrl = "/Images/Products/" + imageFile.FileName;
            }
            _repos.Add(obj);
            return RedirectToAction("Index");
            //  }

            //return View(obj);
        }


        public IActionResult Edit(int Id)
        {

            var listCate = _categories.GetAll();
            ViewBag.CategoryList = new SelectList(listCate, "Id", "CategoryName");
            var product = _repos.GetById(Id);
            return View(product);

        }

        [HttpPost]
        public IActionResult Edit(Product obj, IFormFile imageFile)
        {

            if (imageFile != null && imageFile.Length > 0)
            {
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Products");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var imagePath = Path.Combine(directoryPath, imageFile.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                obj.ImageUrl = "/Images/Products/" + imageFile.FileName;
            }
            _repos.Update(obj);

            return RedirectToAction("Index");

        }


        public IActionResult Delete(int Id)
        {
            _repos.Delete(Id);
            return RedirectToAction("Index");

        }

        public IActionResult Detail(int Id)
        {

            var listCate = _categories.GetAll();
            ViewBag.CategoryList = new SelectList(listCate, "Id", "CategoryName");
            var product = _repos.GetById(Id);
            return View(product);

        }

        public IActionResult NewArrival()
        {
            var list = _products.GetAll();
            return View(list);
        }

    }
}