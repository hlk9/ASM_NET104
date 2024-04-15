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
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(pd => pd.Product)
        .WithOne(id => id.InvoiceDetail)
        .HasForeignKey<InvoiceDetail>(p => p.ProductId)
        .HasConstraintName("FK_InvoiceDetail_ProductDetail")
        .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(i => i.Invoice)
                .WithMany(inv => inv.InvoiceDetails)

                .HasConstraintName("FK_InvoiceDetail_Invoice");
        }
    }
}
