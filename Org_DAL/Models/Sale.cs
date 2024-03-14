using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }
        public DateTime ExpriationDate { get; set; }
        public bool IsActived { get; set; } = true;
         
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
