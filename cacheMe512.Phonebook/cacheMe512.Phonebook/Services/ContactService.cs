
using cacheMe512.Phonebook;
using cacheMe512.Phonebook.Models;
using cacheMe512.Phonebook.Controllers;
using Spectre.Console;
using cacheMe512.Phonebook.Services;

internal class ContactService
{

    internal static void GetContacts()
    {
        var contacts = ContactController.GetContacts();

        if (!contacts.Any())
        {
            Utilities.DisplayMessage("No contacts available.", "cyan");
            Utilities.DisplayMessage("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        UserInterface.ShowContactTable(contacts);
    }

    internal static void GetContact()
    {
        var contact = GetContactOptionInput();

        if (contact == null) return;

        UserInterface.ShowContact(contact);
    }

    internal static void InsertContact()
    {
        var contact = new Contact();

        while (true)
        {
            contact.Name = AnsiConsole.Ask<string>("Enter contact name:");
            if (Validation.IsStringValid(contact.Name))
            {
                break;
            }
        }

        while (true)
        {
            contact.PhoneNumber = AnsiConsole.Ask<string>("Enter phone number (E.164, e.g., +14151231234):");
            if (Validation.IsValidPhoneNumber(contact.PhoneNumber))
            {
                break;
            }
        }

        while (true)
        {
            contact.Email = AnsiConsole.Ask<string>("Enter email (format: local@domain.tld):");
            if (Validation.IsValidEmail(contact.Email))
            {
                break;
            }
        }

        var selectedCategory = CategoryService.GetCategoryOptionInput();

        if (selectedCategory == null)
        {
            Utilities.DisplayMessage("No valid category selected. Contact not created.", "red");
            Utilities.DisplayMessage("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        contact.CategoryId = selectedCategory.CategoryId;

        ContactController.AddContact(contact);
        Utilities.DisplayMessage("Contact added successfully!", "green");
    }


    internal static void UpdateContact()
    {
        var contact = GetContactOptionInput();

        if (contact == null)
        {
            Utilities.DisplayMessage("No contacts available to update.", "cyan");
            Utilities.DisplayMessage("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        var attributesToUpdate = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Enums.ContactAttribute>()
                .Title("Select attribute(s) to update (use <space> to select, <enter> to confirm):")
                .InstructionsText("[grey](Press space to select, enter to finish)[/]")
                .AddChoices(Enum.GetValues(typeof(Enums.ContactAttribute)).Cast<Enums.ContactAttribute>())
        );

        foreach (var attribute in attributesToUpdate)
        {
            switch (attribute)
            {
                case Enums.ContactAttribute.Name:
                    string newName;
                    while (true)
                    {
                        newName = AnsiConsole.Ask<string>("Enter new contact name:");
                        if (Validation.IsStringValid(newName))
                        {
                            break;
                        }
                    }
                    contact.Name = newName;
                    break;

                case Enums.ContactAttribute.PhoneNumber:
                    string newPhone;
                    while (true)
                    {
                        newPhone = AnsiConsole.Ask<string>("Enter new phone number (E.164, e.g., +14151231234):");
                        if (Validation.IsValidPhoneNumber(newPhone))
                        {
                            break;
                        }
                    }
                    contact.PhoneNumber = newPhone;
                    break;

                case Enums.ContactAttribute.Email:
                    string newEmail;
                    while (true)
                    {
                        newEmail = AnsiConsole.Ask<string>("Enter new email (format: local@domain.tld):");
                        if (Validation.IsValidEmail(newEmail))
                        {
                            break;
                        }
                    }
                    contact.Email = newEmail;
                    break;

                case Enums.ContactAttribute.Category:
                    var category = CategoryService.GetCategoryOptionInput();
                    if (category != null)
                    {
                        contact.Category = category;
                    }
                    break;
            }
        }

        ContactController.UpdateContact(contact);
        Utilities.DisplayMessage("Contact updated successfully!", "green");
    }

    internal static void DeleteContact()
    {
        var contact = GetContactOptionInput();
        if (contact == null)
        {
            Utilities.DisplayMessage("No contacts available to delete.", "cyan");
            Utilities.DisplayMessage("\nPress Any Key to Continue.");
            Console.ReadKey();
            return;
        }

        if (!Utilities.ConfirmDeletion(contact))
        {
            Utilities.DisplayMessage("Deletion cancelled.", "cyan");
            return;
        }

        try
        {
            bool deletionSucceeded = ContactController.DeleteContact(contact);
            if (deletionSucceeded)
            {
                Utilities.DisplayMessage("Contact deleted successfully!", "green");
            }
            else
            {
                Utilities.DisplayMessage("Failed to delete contact.", "red");
            }
        }
        catch (Exception ex)
        {
            Utilities.DisplayMessage($"An error occurred during deletion: {ex.Message}", "red");
        }
    }

    static internal Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();

        if (!contacts.Any())
        {
            Utilities.DisplayMessage("No contacts available to select.", "cyan");
            Utilities.DisplayMessage("\nPress any key to continue...");
            Console.ReadKey();
            return null;
        }

        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Contact")
            .AddChoices(contactsArray));

        var id = contacts.Single(x => x.Name == option).ContactId;
        return ContactController.GetContactById(id);
    }

}