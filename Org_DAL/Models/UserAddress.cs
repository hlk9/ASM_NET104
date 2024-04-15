using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class UserAddress
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }    
        public virtual User User { get; set; }
        public string Address { get; set; } 
        public string AddressType { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public byte Status { get; set; } = 1;
    }
}
