using Microsoft.AspNetCore.Mvc;
using Org_DAL.Repository;
using OrganicWebApp.ViewModel;

namespace OrganicWebApp.Controllers
{
    public class InvoiceController : Controller
    {
        InvoiceRepository invoiceRepository = new InvoiceRepository();
        InvoiceDetailRepository invoiceDetailRepository = new InvoiceDetailRepository();
        ProductRepository productRepository = new ProductRepository();

        public IActionResult Index()
        {
            string usrId = HttpContext.Session.GetString("_UserId");
            var list = invoiceRepository.GetAllByUserId(Guid.Parse(usrId));
            return View(list);
        }

        public IActionResult Detail(int id)
        {
            var list = invoiceDetailRepository.GetAll().Where(x => x.InvoiceId == id);
            List<ProductInCart>productInCarts = new List<ProductInCart>();
            foreach(var item in list)
            {
                var product = productRepository.GetById(item.ProductId);

                ProductInCart productIn = new ProductInCart();

                productIn.Quantity = item.Quantity;
                productIn.SaleId = null;
                productIn.Priced = item.Price;
                productIn.Total = item.Total;
                productIn.ImageUrl = product.ImageUrl;
                productIn.ProductName = product.ProductName;
                productIn.Weight = product.Weight;

                productInCarts.Add(productIn);


            }

            return View(productInCarts);
        }
    }
}
