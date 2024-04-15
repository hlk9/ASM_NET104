using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
           builder.HasKey(x => x.Id);
            builder.HasOne(u => u.User).WithMany(i => i.Invoices).HasConstraintName("FK_Invoice_User");         

            builder.HasOne(c => c.Coupon).WithMany(i => i.Invoices).HasConstraintName("FK_Invoice_Coupon");
        }
    }
}
