using AuthAspRazorPages.Models.Article;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.Models.Book;

namespace AuthAspRazorPages.EFcore.Mapping
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Price).IsRequired();

        }
    }
}
