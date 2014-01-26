using System.Data.Entity.ModelConfiguration;
using Waf.BookLibrary.Library.Domain;

namespace Waf.BookLibrary.Library.Applications.Data
{
    internal class BookMapping : EntityTypeConfiguration<Book>
    {
        public BookMapping()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title").HasMaxLength(100);
            this.Property(t => t.Author).HasColumnName("Author").HasMaxLength(100);
            this.Property(t => t.Publisher).HasColumnName("Publisher").HasMaxLength(100);
            this.Property(t => t.PublishDate).HasColumnName("PublishDate");
            this.Property(t => t.Isbn).HasColumnName("Isbn").HasMaxLength(14);
            this.Property(t => t.Language).HasColumnName("Language");
            this.Property(t => t.Pages).HasColumnName("Pages");

            this.HasOptional(t => t.LendTo).WithMany().Map(t => t.MapKey("PersonId"));

            this.ToTable("Book");
        }
    }
}
