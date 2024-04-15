using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public double Discount { get; set; }
        public DateTime ExpriationDate { get; set; }
        public bool IsActived { get; set; } = true;
        public ICollection<Invoice> Invoices { get; set; }
    }
}
