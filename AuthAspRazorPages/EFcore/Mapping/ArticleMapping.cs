using AuthAspRazorPages.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.Models.Article;

namespace AuthAspRazorPages.EFcore.Mapping
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {

        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Story).HasMaxLength(600).IsRequired();

        }
    }
}
