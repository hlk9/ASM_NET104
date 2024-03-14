using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate {  get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set;}
        public byte Status { get; set; } = 1;

        public ICollection<UserRole> Roles { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
        public ICollection<Invoice> Invoices { get; set; }

    }
}
