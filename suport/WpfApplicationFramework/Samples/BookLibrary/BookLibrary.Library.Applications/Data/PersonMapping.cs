using System.Data.Entity.ModelConfiguration;
using Waf.BookLibrary.Library.Domain;

namespace Waf.BookLibrary.Library.Applications.Data
{
    internal class PersonMapping : EntityTypeConfiguration<Person>
    {
        public PersonMapping()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Firstname).HasMaxLength(30).HasColumnName("Firstname");
            this.Property(t => t.Lastname).HasMaxLength(30).HasColumnName("Lastname");
            this.Property(t => t.Email).HasMaxLength(100).HasColumnName("Email");

            this.ToTable("Person");
        }
    }
}
