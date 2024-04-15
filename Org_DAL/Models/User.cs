using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string UserName { get; set; }
        public string FullName { get; set; }
        [AllowNull]
        public DateTime? DateOfBirth { get; set; }
        [AllowNull]
        public bool? Gender { get; set; }
        public string Email { get; set; }
        [AllowNull]
        [MaxLength(30)]
        public string? Phone { get; set; }
        public string Password { get; set; }
        [AllowNull]
        public DateTime? CreatedDate {  get; set; } = DateTime.UtcNow;
        [AllowNull]
        public DateTime? UpdatedDate { get; set;}
        public byte Status { get; set; } = 1;

        public ICollection<UserRole> UserRole { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
        public ICollection<Invoice> Invoices { get; set; }

        public virtual Cart Cart { get; set; }

 

    }
}
