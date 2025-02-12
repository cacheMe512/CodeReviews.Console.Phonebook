using cacheMe512.Phonebook.Controllers;
using cacheMe512.Phonebook.Models;
using Spectre.Console;

namespace cacheMe512.Phonebook;

internal class Utilities
{
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
