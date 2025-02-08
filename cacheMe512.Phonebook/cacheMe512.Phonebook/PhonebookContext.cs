using Microsoft.EntityFrameworkCore;

namespace cacheMe512.Phonebook;

internal class PhonebookContext: DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(LocalDb)\\mssqllocaldb;Database=Phonebook;Trusted_Connection=True;");
    }
}
