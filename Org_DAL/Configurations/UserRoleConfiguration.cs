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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
           builder.HasKey(x => x.Id);
            builder.HasOne(u => u.User).WithMany(ur => ur.UserRole).HasConstraintName("FK_UserRole_User");
            builder.HasOne(r => r.Role).WithMany(r => r.UserRoles).HasConstraintName("FK_UserRole_Role");
            
        }
    }
}
