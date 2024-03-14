using Microsoft.EntityFrameworkCore;
using Org_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Context
{
    public class OrganicDbContext:DbContext
    {

        public OrganicDbContext()
        {
            
        }

        public OrganicDbContext(DbContextOptions<OrganicDbContext> options) : base(options) 
        {

        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoicesDetail { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Organic;Integrated Security=True");
        }

    }
}
