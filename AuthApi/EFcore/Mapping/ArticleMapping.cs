

using AuthApi.Models.Article;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthApi.EFcore.Mapping
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
