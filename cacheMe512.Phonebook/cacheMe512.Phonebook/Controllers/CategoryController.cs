using cacheMe512.Phonebook.Models;
using Microsoft.EntityFrameworkCore;

namespace cacheMe512.Phonebook.Controllers;

internal class CategoryController
{
    internal static void AddCategory(Category category)
    {
        using var db = new PhonebookContext();

        db.Add(category);

        db.SaveChanges();
    }

    internal static List<Category> GetCategories()
    {
        using var db = new PhonebookContext();

        var categories = db.Categories
            .Include(x => x.Contacts)
            .ToList();

        return categories;

    }

    internal static void UpdateCategory(Category category)
    {
        using var db = new PhonebookContext();

        db.Update(category);

        db.SaveChanges();
    }

    internal static void DeleteCategory(Category category)
    {
        using var db = new PhonebookContext();

        db.Remove(category);

        db.SaveChanges();
    }
}
