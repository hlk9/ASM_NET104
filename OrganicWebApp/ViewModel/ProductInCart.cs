using Org_DAL.Models;
using System.ComponentModel;

namespace OrganicWebApp.ViewModel
{
    public class ProductInCart
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double? Weight { get; set; }
        public int Quantity { get; set; }
        public double Priced { get; set; }
        public int? SaleId { get; set; }
        public string? ProductDescription { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string? TypeClosure { get; set; }

        public double Total {  get; set; }  

        public int CartItemID { get; set; }


    }
}
