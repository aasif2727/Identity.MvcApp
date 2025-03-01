using Identity.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.EntityConfigs
{
    public class ApplicationUserConfig: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(p => p.FirstName).IsRequired(true).HasMaxLength(100);
            builder.Property(p => p.LastName).IsRequired(true).HasMaxLength(100);
            builder.Property(p => p.DateCreated).IsRequired(true).HasDefaultValueSql("getdate()");
            builder.Ignore(p => p.RoleId);
            builder.Ignore(p => p.Role);
            builder.Ignore(p => p.UserClaim);
            builder.ToTable("ApplicationUser");
        }
    }
}
