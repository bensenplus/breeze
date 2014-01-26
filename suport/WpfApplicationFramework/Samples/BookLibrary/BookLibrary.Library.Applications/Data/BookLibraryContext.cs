using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;

namespace Waf.BookLibrary.Library.Applications.Data
{
    internal class BookLibraryContext : DbContext
    {
        public BookLibraryContext(DbConnection dbConnection) : base(dbConnection, false)
        {
            Database.SetInitializer<BookLibraryContext>(null);
        }

        public bool HasChanges 
        {
            get 
            {
                ChangeTracker.DetectChanges();

                // It is necessary to ask the ObjectContext if changes could be detected because the
                // DbContext does not provide the information when a navigation property has changed.
                return ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added).Any()
                    || ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified).Any()
                    || ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted).Any();
            } 
        }

        private ObjectContext ObjectContext { get { return ((IObjectContextAdapter)this).ObjectContext; } }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new PersonMapping());
            modelBuilder.Configurations.Add(new BookMapping());
        }
    }
}
