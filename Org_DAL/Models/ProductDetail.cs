using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public double? Weight { get; set; }
        public string? TypeClosure {  get; set; }
        public DateTime EXP {  get; set; }
        public DateTime MFG { get; set; }
        public int Quantity { get; set; }
        public double Priced { get; set; }
        public double ListedPriced { get; set; }
        public int SaleId { get; set; }
        [ForeignKey("SaleId")]
        public virtual Sale Sale { get; set; }
        public byte Status { get; set; } = 1;


    }
}

