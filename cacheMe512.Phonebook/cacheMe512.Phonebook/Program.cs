using Spectre.Console;

var isAppRunning = true;
while (isAppRunning)
{
    var option = AnsiConsole.Prompt(
    new SelectionPrompt<MenuOptions>()
    .Title("What would you like to do?")
    .AddChoices(
        MenuOptions.AddContact,
        MenuOptions.DeleteContact,
        MenuOptions.UpdateContact,
        MenuOptions.ViewAllContacts,
        MenuOptions.ViewContact));

    switch (option)
    {
        case MenuOptions.AddContact:
            PhonebookService.InsertContact();
            break;
        case MenuOptions.DeleteContact:
            PhonebookService.DeleteContact();
            break;
        case MenuOptions.UpdateContact:
            PhonebookService.UpdateContact();
            break;
        case MenuOptions.ViewContact:
            PhonebookService.GetContact();
            break;
        case MenuOptions.ViewAllContacts:
            PhonebookService.GetContacts();
            break;
    }
}


enum MenuOptions
{
    AddContact,
    DeleteContact,
    UpdateContact,
    ViewContact,
    ViewAllContacts,
    Quit
}