using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }
        public int ProductDetailId { get; set; }
        [ForeignKey("ProductDetailId")]
        public virtual ProductDetail ProductDetail { get; set; }
        public int Quantity { get; set; }
        public byte Status { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }

    }
}
