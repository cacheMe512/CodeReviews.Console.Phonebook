
using cacheMe512.Phonebook;
using Spectre.Console;

internal class PhonebookService
{

    internal static void GetContacts()
    {
        var contacts = PhonebookController.GetContacts();
        UserInterface.ShowContactTable(contacts);
    }

    internal static void GetContact()
    {
        var contact = Utilities.GetContactOptionInput();
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

        PhonebookController.AddContact(contact);
    }

    internal static void UpdateContact()
    {
        var contact = Utilities.GetContactOptionInput();

        var attributesToUpdate = AnsiConsole.Prompt(
            new MultiSelectionPrompt<ContactAttribute>()
                .Title("Select attribute(s) to update (use <space> to select, <enter> to confirm):")
                .InstructionsText("[grey](Press space to select, enter to finish)[/]")
                .AddChoices(Enum.GetValues(typeof(ContactAttribute)).Cast<ContactAttribute>())
        );

        foreach (var attribute in attributesToUpdate)
        {
            switch (attribute)
            {
                case ContactAttribute.Name:
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

                case ContactAttribute.PhoneNumber:
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

                case ContactAttribute.Email:
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
            }
        }

        PhonebookController.UpdateContact(contact);
    }

    internal static void DeleteContact()
    {
        var contact = Utilities.GetContactOptionInput();
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
            bool deletionSucceeded = PhonebookController.DeleteContact(contact);
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
}