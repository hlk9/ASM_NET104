using Microsoft.AspNetCore.Mvc;
using Org_DAL.Models;
using Org_DAL.Repository;
using OrganicWebApp.ViewModel;

namespace OrganicWebApp.Controllers
{
    public class CheckoutController : Controller
    {
        InvoiceRepository invoiceRepository = new InvoiceRepository();
        InvoiceDetailRepository InvoiceDetailRepository = new InvoiceDetailRepository();
        CartItemRepository cartItemRepository = new CartItemRepository();
        ProductRepository productRepository = new ProductRepository();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Check()
        {
            string usrId = HttpContext.Session.GetString("_UserId");

            Invoice inv = new Invoice();
            inv.UserId = Guid.Parse(usrId);
            inv.TotalAmount = 0;
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
                    invoiceDetail.Quantity = product.Quantity;
                    invoiceDetail.Total = product.Quantity * product.Priced;
                    invoiceDetail.InvoiceId = inv.Id;
                    lstinvoice.Add(invoiceDetail);
                    total += invoiceDetail.Total;
                }
            }

            foreach(var item in lstCart)
            {
                cartItemRepository.Delete(item.Id);
            }    

            return RedirectToAction("Index", "Checkout");
        }
    }
}
