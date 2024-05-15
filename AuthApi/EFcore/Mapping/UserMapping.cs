
using AuthApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthApi.EFcore.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
      
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();

            builder.Property(x => x.FName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.ProfilePhoto).HasMaxLength(500).IsRequired(false);


            builder.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
        }
    }
}
