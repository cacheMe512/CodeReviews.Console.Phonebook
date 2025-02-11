using Spectre.Console;

namespace cacheMe512.Phonebook;

internal class Utilities
{
    static internal Contact GetContactOptionInput()
    {
        var contacts = PhonebookController.GetContacts();
        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(x => x.Name == option).Id;
        var contact = PhonebookController.GetContactById(id);

        return contact;
    }

    public static bool ConfirmDeletion(Contact contact)
    {
        var confirm = AnsiConsole.Confirm($"[red]Are you sure you want to delete contact: {contact.Name}?[/]");

        return confirm;
    }

    public static void DisplayMessage(string message, string color = "yellow")
    {
        AnsiConsole.MarkupLine($"[{color}]{message}[/]");
    }
}


public enum ContactAttribute
{
    Name,
    PhoneNumber,
    Email
}
