using Spectre.Console;

namespace cacheMe512.Phonebook;

internal class UserInterface
{
    internal static void ShowContact(Contact contact)
    {
        var panel = new Panel($@"Id: {contact.Id}
Name: {contact.Name}
Phone Number: {contact.PhoneNumber}
Email: {contact.Email}");
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

        foreach (Contact contact in contacts)
        {
            table.AddRow(
                contact.Id.ToString(),
                contact.Name,
                contact.PhoneNumber,
                contact.Email
                );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Enter any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }
}
