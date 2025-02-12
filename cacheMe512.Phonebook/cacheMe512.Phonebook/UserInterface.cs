using cacheMe512.Phonebook.Models;
using cacheMe512.Phonebook.Services;
using Spectre.Console;
using static cacheMe512.Phonebook.Enums;

namespace cacheMe512.Phonebook;

internal class UserInterface
{
    static internal void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MainMenuOptions.ManageCategories,
                MainMenuOptions.ManageContacts,
                MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.ManageCategories:
                    CategoriesMenu();
                    break;
                case MainMenuOptions.ManageContacts:
                    ContactsMenu();
                    break;
                case MainMenuOptions.Quit:
                    Console.WriteLine("Goodbye");
                    isAppRunning = false;
                    break;
            }
        }
    }

    static internal void CategoriesMenu()
    {
        var isCategoriesMenuRunning = true;
        while (isCategoriesMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<CategoryMenu>()
            .Title("Categories Menu")
            .AddChoices(
                CategoryMenu.AddCategory,
                CategoryMenu.DeleteCategory,
                CategoryMenu.UpdateCategory,
                CategoryMenu.ViewAllCategories,
                CategoryMenu.ViewCategory,
                CategoryMenu.GoBack));

            switch (option)
            {
                case CategoryMenu.AddCategory:
                    CategoryService.InsertCategory();
                    break;
                case CategoryMenu.DeleteCategory:
                    CategoryService.DeleteCategory();
                    break;
                case CategoryMenu.UpdateCategory:
                    CategoryService.UpdateCategory();
                    break;
                case CategoryMenu.ViewAllCategories:
                    CategoryService.GetCategories();
                    break;
                case CategoryMenu.ViewCategory:
                    CategoryService.GetCategory();
                    break;
                case CategoryMenu.GoBack:
                    isCategoriesMenuRunning = false;
                    break;
            }
        }
    }

    static internal void ContactsMenu()
    {
        var isContactMenuRunning = true;
        while (isContactMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<ContactMenu>()
            .Title("Contacts Menu")
            .AddChoices(
                ContactMenu.AddContact,
                ContactMenu.DeleteContact,
                ContactMenu.UpdateContact,
                ContactMenu.ViewAllContacts,
                ContactMenu.ViewContact,
                ContactMenu.GoBack));

            switch (option)
            {
                case ContactMenu.AddContact:
                    ContactService.InsertContact();
                    break;
                case ContactMenu.DeleteContact:
                    ContactService.DeleteContact();
                    break;
                case ContactMenu.UpdateContact:
                    ContactService.UpdateContact();
                    break;
                case ContactMenu.ViewContact:
                    ContactService.GetContact();
                    break;
                case ContactMenu.ViewAllContacts:
                    ContactService.GetContacts();
                    break;
                case ContactMenu.GoBack:
                    isContactMenuRunning = false;
                    break;
            }
        }
    }

    internal static void ShowCategoryTable(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (Category category in categories)
        {
            table.AddRow(
                category.CategoryId.ToString(),
                category.Name
                );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press Any Key to Return to Menu");
        Console.ReadLine();
        Console.Clear();
    }

    static internal void ShowCategory(Category category)
    {
        var panel = new Panel($@"Id: {category.CategoryId}
Contacts: {category.Contacts.Count}");
        panel.Header = new PanelHeader($"{category.Name}");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        ShowContactTable(category.Contacts);

        Console.WriteLine("Press Any Key to Return to Menu");
        Console.ReadLine();
        Console.Clear();
    }

    internal static void ShowContact(Contact contact)
    {
        var panel = new Panel($@"Id: {contact.ContactId}
Name: {contact.Name}
Phone Number: {contact.PhoneNumber}
Email: {contact.Email}
Category: {contact.Category.Name}");
        panel.Header = new PanelHeader("Contact Info");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Enter any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }

    static internal void ShowContactTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");
        table.AddColumn("Category");

        foreach (Contact contact in contacts)
        {
            table.AddRow(
                contact.ContactId.ToString(),
                contact.Name,
                contact.PhoneNumber,
                contact.Email,
                contact.Category.Name
                );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Enter any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }
}
