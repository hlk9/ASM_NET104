using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class Role
    {
        [Key]
       public  int Id { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
