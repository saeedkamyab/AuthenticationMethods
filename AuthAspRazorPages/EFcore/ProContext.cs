using AuthAspRazorPages.EFcore.Mapping;
using AuthAspRazorPages.Models;
using AuthAspRazorPages.Models.RoleAndPermission;
using Microsoft.EntityFrameworkCore;

namespace AuthAspRazorPages.EFcore
{
    public class ProContext : DbContext
    {

        public ProContext(DbContextOptions<ProContext> options) : base(options)
        {

        }
        //      public ProContext(DbContextOptions options)
        //: base(options)
        //      {
        //      }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(UserMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
