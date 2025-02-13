using cacheMe512.Phonebook.Controllers;
using cacheMe512.Phonebook.Models;
using Spectre.Console;

namespace cacheMe512.Phonebook.Services;

internal class CategoryService
{
    internal static void GetCategories()
    {
        var categories = CategoryController.GetCategories();

        if (!categories.Any())
        {
            Utilities.DisplayMessage("No categories available.", "cyan");
            Utilities.DisplayMessage("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        UserInterface.ShowCategoryTable(categories);
    }

    internal static void GetCategory()
    {
        var category = GetCategoryOptionInput();

        if (category == null) return;

        UserInterface.ShowCategory(category);
    }

    internal static void InsertCategory()
    {
        var category = new Category();

        category.Name = AnsiConsole.Ask<string>("Category's name:");

        CategoryController.AddCategory(category);
    }

    internal static void UpdateCategory()
    {
        var category = GetCategoryOptionInput();

        if (category == null)
        {
            Utilities.DisplayMessage("No categories available to update.", "cyan");
            Utilities.DisplayMessage("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        category.Name = AnsiConsole.Ask<string>("Enter the new category name:");

        CategoryController.UpdateCategory(category);
        Utilities.DisplayMessage("Category updated successfully!", "green");
    }

    internal static void DeleteCategory()
    {
        var category = GetCategoryOptionInput();
        CategoryController.DeleteCategory(category);
    }

    internal static Category GetCategoryOptionInput()
    {
        var categories = CategoryController.GetCategories();

        if (!categories.Any())
        {
            Utilities.DisplayMessage("No categories available to select.", "cyan");
            Utilities.DisplayMessage("\nPress any key to continue...");
            Console.ReadKey();
            return null;
        }

        var categoriesArray = categories.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Category")
            .AddChoices(categoriesArray));

        return categories.Single(x => x.Name == option);
    }

}
