
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
        throw new NotImplementedException();
    }

    internal static void DeleteContact()
    {
        throw new NotImplementedException();
    }


}