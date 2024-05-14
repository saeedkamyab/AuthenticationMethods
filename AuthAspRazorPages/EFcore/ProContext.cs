using AuthAspRazorPages.EFcore.Mapping;
using AuthAspRazorPages.Models;
using AuthAspRazorPages.Models.Article;
using AuthAspRazorPages.Models.Book;
using AuthAspRazorPages.Models.RoleAndPermission;
using Microsoft.EntityFrameworkCore;

namespace AuthAspRazorPages.EFcore
{
    public partial class ProContext : DbContext
    {
        public ProContext() { }
        //public ProContext(DbContextOptions<ProContext> options) : base(options)
        //{
                                     //*******I had a very bad error here becuse of having this constructor and not having empty and below constructor********
        //}
        public ProContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            var assembly = typeof(UserMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
