
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
        UserInterface.ShowContactTable(contacts);
    }

    internal static void GetContact()
    {
        var contact = GetContactOptionInput();
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

        contact.CategoryId = CategoryService.GetCategoryOptionInput().CategoryId;

        ContactController.AddContact(contact);
    }

    internal static void UpdateContact()
    {
        var contact = GetContactOptionInput();

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

                    contact.CategoryId = CategoryService.GetCategoryOptionInput().CategoryId;

                    break;
            }
        }

        ContactController.UpdateContact(contact);
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
        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(x => x.Name == option).ContactId;
        var contact = ContactController.GetContactById(id);

        return contact;
    }
}