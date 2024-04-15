using Microsoft.AspNetCore.Mvc;
using Org_DAL.Models;
using Org_DAL.Repository;
using OrganicWebApp.ViewModel;

namespace OrganicWebApp.Controllers
{
    public class CartController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        CartItemRepository cartItemRepository = new CartItemRepository();
        InvoiceRepository invoiceRepository = new InvoiceRepository();
        InvoiceDetailRepository InvoiceDetailRepository = new InvoiceDetailRepository();
        private readonly IHttpContextAccessor _contextAccessor;

        public CartController(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            int cartId = Convert.ToInt32(HttpContext.Session.GetInt32("_CartId"));
            List<CartItem> lstCart = new List<CartItem>();
            List<ProductInCart> lstPrdInCart = new List<ProductInCart>();

            lstCart = cartItemRepository.GetAllByIdCard(cartId);

            if (lstCart.Count > 0)
            {
                foreach (CartItem item in lstCart)
                {
                    var product = productRepository.GetById(item.ProductId);

                    ProductInCart prd = new ProductInCart();
                    prd.Quantity = item.Quantity;
                    prd.Id = product.Id;
                    prd.ProductName = product.ProductName;
                    prd.SaleId = product.SaleId;
                    prd.Priced = product.Priced;
                    prd.Total = product.Priced * item.Quantity;
                    prd.CategoryId = product.CategoryId;
                    prd.ImageUrl = product.ImageUrl;
                    prd.Weight = product.Weight;
                    prd.ProductDescription = product.ProductDescription;
                    prd.TypeClosure = product.TypeClosure;
                    prd.CartItemID = item.Id;
                    lstPrdInCart.Add(prd);
                }
            }

            double total = 0;

            foreach (var item in lstPrdInCart)
            {
                total += item.Priced * item.Quantity;


            }

            ViewData["Total"] = total;
            _contextAccessor.HttpContext.Session.SetString("TotalInCartString", cartItemRepository.GetAllByIdCard(cartId).Count.ToString());

            return View(lstPrdInCart);
        }

        public IActionResult Check(string totalInvoice)
        {
            if(double.Parse(totalInvoice) <=0)
            {
                ViewBag.Error = "Lỗi";
                return RedirectToAction("Index", "Cart");
            }    

            double totalP = Convert.ToDouble(totalInvoice);
            string usrId = HttpContext.Session.GetString("_UserId");

            Invoice inv = new Invoice();
            inv.UserId = Guid.Parse(usrId);
            inv.TotalAmount = totalP;
            inv.Status = 1;
            inv.UpdateAt = null;
            inv.CouponId = -1;
            invoiceRepository.Add(inv);


            int cartId = Convert.ToInt32(HttpContext.Session.GetInt32("_CartId"));
            List<CartItem> lstCart = new List<CartItem>();
            List<InvoiceDetail> lstinvoice = new List<InvoiceDetail>();

            lstCart = cartItemRepository.GetAllByIdCard(cartId);
            double total = 0;
            if (lstCart.Count > 0)
            {
                foreach (CartItem item in lstCart)
                {
                    var product = productRepository.GetById(item.ProductId);
                    InvoiceDetail invoiceDetail = new InvoiceDetail();
                    invoiceDetail.ProductId = product.Id;
                    invoiceDetail.Price = product.Priced;
                    invoiceDetail.Quantity = item.Quantity;
                    invoiceDetail.Total = item.Quantity * product.Priced;
                    invoiceDetail.InvoiceId = inv.Id;
                    InvoiceDetailRepository.Add(invoiceDetail);
                    total += invoiceDetail.Total;
                }
            }

            foreach (var item in lstCart)
            {
                cartItemRepository.Delete(item.Id);
            }
            _contextAccessor.HttpContext.Session.SetInt32("TotalInCart", cartItemRepository.GetAllByIdCard(cartId).Count);
            _contextAccessor.HttpContext.Session.SetString("TotalInCartString", cartItemRepository.GetAllByIdCard(cartId).Count.ToString());

            return RedirectToAction("Index", "Invoice");
        }

        public IActionResult DeleteCartItem(int id)
        {
            int cartId = Convert.ToInt32(HttpContext.Session.GetInt32("_CartId"));



            cartItemRepository.Delete(id);
            _contextAccessor.HttpContext.Session.SetInt32("TotalInCart", cartItemRepository.GetAllByIdCard(cartId).Count);
            _contextAccessor.HttpContext.Session.SetString("TotalInCartString", cartItemRepository.GetAllByIdCard(cartId).Count.ToString());

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult IncreaseCartItem(int id)
        {
            int cartId = Convert.ToInt32(HttpContext.Session.GetInt32("_CartId"));
            var a = cartItemRepository.GetById(id);

            a.Quantity += 1;
            if (productRepository.GetById(a.ProductId).Quantity < a.Quantity)
            {
                ViewBag.Error = "Số lượng vượt quá giới hạn";
            }
            else
            {
                cartItemRepository.Update(a);
            }
            _contextAccessor.HttpContext.Session.SetInt32("TotalInCart", cartItemRepository.GetAllByIdCard(cartId).Count);
            _contextAccessor.HttpContext.Session.SetString("TotalInCartString", cartItemRepository.GetAllByIdCard(cartId).Count.ToString());

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult DecreaseCartItem(int id)
        {
            int cartId = Convert.ToInt32(HttpContext.Session.GetInt32("_CartId"));
            var a = cartItemRepository.GetById(id);

            a.Quantity -= 1;
            if(a.Quantity<=0)
            {
                cartItemRepository.Delete(id);
            }    
            else
            {
                cartItemRepository.Update(a);
            }
            _contextAccessor.HttpContext.Session.SetInt32("TotalInCart", cartItemRepository.GetAllByIdCard(cartId).Count);
            _contextAccessor.HttpContext.Session.SetString("TotalInCartString", cartItemRepository.GetAllByIdCard(cartId).Count.ToString());

            return RedirectToAction("Index", "Cart");
        }
    }
}

