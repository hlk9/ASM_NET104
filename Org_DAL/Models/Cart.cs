using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }     
        public virtual User User { get; set; }
        public ICollection<CartItem> CartItem { get; set;}
    }
}
