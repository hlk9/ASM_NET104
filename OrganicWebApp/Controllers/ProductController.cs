using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org_DAL.Models;
using Org_DAL.Repository;

namespace OrganicWebApp.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly ProductRepository _repos = new ProductRepository();
        public readonly CategoryRepository _categories = new CategoryRepository();
        private CartItemRepository _cartItemRepository = new CartItemRepository();
        private CartRepository _cartRepository = new CartRepository();
        ProductRepository _products = new ProductRepository();


        private readonly IHttpContextAccessor _httpContextAccessor;


     

        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


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

        public IActionResult Detail_C(int id)
        {
            var prod =  _products.GetById(id);
            if(prod == null)
            {
                return RedirectToAction("NewArrival", "Product");
            }
            return View(prod);
        }

        public IActionResult AddToCart(string id,int qty)
        {

            ProductRepository productRepository = new ProductRepository();

            var prd = productRepository.GetById(int.Parse(id));

            if(prd.Quantity<qty)
            {
                ViewBag.Error = "Số lượng vượt giới hạn, hãy điều chỉnh lại";
                return RedirectToAction("Detail_C", "Product", new { id = id });
            }    
            else
            {
                ViewBag.Error = null;
            }    

            string usrId = HttpContext.Session.GetString("_UserId");
            if(usrId!=null)
            {

                var cart = _cartRepository.GetbyUserId(Guid.Parse(usrId));
                CartItem item = new CartItem();
                item.CartId = cart.Id;
                item.ProductId = int.Parse(id);
                item.Quantity = qty;
                item.Status = 1;
                
              
                var inDb = _cartItemRepository.Contain_CID_And_PID(cart.Id, int.Parse(id));
                if (inDb!=null)
                {
                    inDb.Quantity += qty;
                    if (inDb.Quantity < qty)
                    {
                        ViewBag.Error = "Số lượng vượt giới hạn, hãy điều chỉnh lại";
                        return RedirectToAction("Detail_C", "Product", new { id = id });
                    }
                    else
                    {
                        ViewBag.Error = null;
                    }
                    _cartItemRepository.Update(inDb);
                    HttpContext.Session.SetInt32("TotalInCart", _cartItemRepository.GetAllByIdCard(cart.Id).Count);
                    _httpContextAccessor.HttpContext.Session.SetString("TotalInCartString", _cartItemRepository.GetAllByIdCard(cart.Id).Count.ToString());
                    return RedirectToAction("Detail_C", "Product", new { id = id });
                }    

                _cartItemRepository.Add(item);

                HttpContext.Session.SetInt32("TotalInCart", _cartItemRepository.GetAllByIdCard(cart.Id).Count);
                _httpContextAccessor.HttpContext.Session.SetString("TotalInCartString", _cartItemRepository.GetAllByIdCard(cart.Id).Count.ToString());


            }
            else
            {
                //cookies
                string listRaw = "";
                List<CartItem> cartItems = new List<CartItem>();

                if (Request.Cookies.TryGetValue("cartData", out listRaw))
                {
                    CartItem item = new CartItem();
                    item.CartId = -1;
                    item.ProductId = int.Parse(id);
                    item.Quantity = qty;
                    item.Status = 1;

                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddMonths(1);
                    options.IsEssential = true;
                    options.Path = "/";

                    cartItems = JsonConvert.DeserializeObject<List<CartItem>>(listRaw);

                    var existingItem = cartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += item.Quantity;
                        HttpContext.Session.SetString("TotalInCart", cartItems.Count.ToString());
                        _httpContextAccessor.HttpContext.Session.SetString("TotalInCartString", _cartItemRepository.GetAllByIdCard(cartItems.Count).Count.ToString());
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("cartData", JsonConvert.SerializeObject(cartItems), options);
                        return RedirectToAction("Detail_C", "Product", new { id = id });
                    }
                    else
                    {
                    cartItems.Add(item);
                    }    
                    

                    HttpContext.Session.SetString("TotalInCart", cartItems.Count.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("TotalInCartString", _cartItemRepository.GetAllByIdCard(cartItems.Count).Count.ToString());
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("cartData", JsonConvert.SerializeObject(cartItems), options);
                   
                }
            }    
            return RedirectToAction("Detail_C", "Product", new { id = id });
        }

    }
}