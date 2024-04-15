using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class InvoiceDetail
    {
        [Key]
      public int Id { get; set; }
        public int InvoiceId {  get; set; }
     
        public virtual Invoice Invoice { get; set; }
        public int ProductId { get; set; }
     
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set;}
    }
}
