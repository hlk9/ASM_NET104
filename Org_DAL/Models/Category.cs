using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public byte Status { get; set; } = 1;

        public ICollection<Product> Products { get; set; }
         
    }
}
