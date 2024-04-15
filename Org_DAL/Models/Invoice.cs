using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
      
        public virtual User User { get; set; }
        public double TotalAmount { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public byte Status { get; set; } = 1;
        public int? CouponId { get; set; }
    
        public virtual Coupon Coupon { get; set; }
        public DateTime? UpdateAt { get ; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }

    }
}
