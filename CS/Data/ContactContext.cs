using System.Data.Entity;

namespace DataAnnotationAttributes.Model {
    public class ContactContext : DbContext {
        static ContactContext() {
            Database.SetInitializer<ContactContext>(new ContactContextInitializer());
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}