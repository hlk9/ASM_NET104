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
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
           builder.HasKey(x => x.Id);
            builder.HasOne(c => c.Cart).WithMany(ct => ct.CartItem).HasConstraintName("FK_CartItem_Cart");
            builder.HasOne(pd => pd.ProductDetail).WithOne(ci => ci.CartItem).HasForeignKey<CartItem>(p => p.ProductDetailId).HasConstraintName("FK_CartItem_ProductDetail");
       


        }
    }
}
