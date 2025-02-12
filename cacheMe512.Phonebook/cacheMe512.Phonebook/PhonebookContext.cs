using cacheMe512.Phonebook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace cacheMe512.Phonebook;

internal class PhonebookContext: DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(LocalDb)\\mssqllocaldb;Database=Phonebook;Trusted_Connection=True;")
                      .ConfigureWarnings(warnings =>
                            warnings.Ignore(RelationalEventId.NonTransactionalMigrationOperationWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = 1,
                Name = "Default"
            }
        );
    }
}
